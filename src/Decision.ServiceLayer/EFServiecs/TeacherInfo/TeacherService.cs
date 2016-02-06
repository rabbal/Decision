using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Home;
using Decision.ViewModel.Applicant;
using Decision.ViewModel.ReferentialApplicant;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده سرویس دهنده عملیات مرتبط با متقاضی
    /// </summary>
    public class ApplicantService : IApplicantService
    {
        #region Fields

        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string DefaultAvatarPath = "~/Content/Images/default-avatar.png";
        private readonly ITitleService _titleService;
        private readonly ITrainingCenterService _trainingCenterService;
        private readonly ITrainingCourseService _trainingCourseService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Applicant> _Applicants;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor

        public ApplicantService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager, ITrainingCenterService trainingCenter, ITrainingCourseService trainingCourse,
            IMappingEngine mappingEngine, IReferentialApplicantService referentialApplicantService, ITitleService titleService, IStateService stateService, ICityService cityService
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _Applicants = _unitOfWork.Set<Applicant>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
            _titleService = titleService;
            _httpContextBase = httpContextBase;
            _referentialApplicantService = referentialApplicantService;
            _trainingCenterService = trainingCenter;
            _trainingCourseService = trainingCourse;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _Applicants.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async Task<EditApplicantViewModel> GetForEditAsync(Guid id, string path)
        {
            var viewModel =
                await
                    _Applicants.AsNoTracking()
                        .ProjectTo<EditApplicantViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);

            if (viewModel == null) return null;

            viewModel.StatesForBirthPlace = _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);
            viewModel.CitiesForBirthPlace = _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                viewModel.BirthPlaceCity, path);


            if (viewModel.TrainingCourseId.HasValue)
            {
                var course = await _trainingCourseService.Get(viewModel.TrainingCourseId.Value);
                viewModel.TrainingCenters =
                    await
                        _trainingCenterService.GetAsSelectListItemAsync(course.TrainingCenter.City,
                            course.TrainingCenterId);
                viewModel.TrainingCourses =
                    await
                        _trainingCourseService.GetAsSelectListByTrainingCenterIdAsync(course.TrainingCenterId,
                            viewModel.TrainingCourseId);

                viewModel.StatesForTrainingCeneter = _stateService.GetAsSelectListItemAsync(
                    course.TrainingCenter.State, path);
                viewModel.CitiesForTrainingCeneter =
                    _cityService.GetAsSelectListByStateNameAsync(course.TrainingCenter.State, course.TrainingCenter.City,
                        path);
            }
            else
            {
                viewModel.StatesForTrainingCeneter = _stateService.GetAsSelectListItemAsync(
                  null, path);
            }

            viewModel.Positions =
                await _titleService.GetAsSelectListItemAsync(TitleType.ApplicantPosition, viewModel.PositionId);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditApplicantViewModel viewModel)
        {
            var Applicant = await _Applicants.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, Applicant);

            if (viewModel.PhotoScan.HasValue())
                Applicant.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                Applicant.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                Applicant.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                Applicant.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }


            if (viewModel.CopyOfNationalCardScan.HasValue())
                Applicant.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                Applicant.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
            Applicant.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public void Create(AddApplicantViewModel viewModel)
        {
            var Applicant = _mappingEngine.Map<Applicant>(viewModel);
            Applicant.CreatorId = _userManager.GetCurrentUserId();

            if (_userManager.IsAdministrator())
            {
                Applicant.IsInReference = false;
                Applicant.IsApproved = true;
                Applicant.ApproveById = Applicant.CreatorId;
            }
            else if (_userManager.IsOperator())
            {
                _referentialApplicantService.Create(new AddReferentialApplicantViewModel
                {
                    ApplicantId = Applicant.Id,
                    ReferencedToId = Applicant.CreatorId
                });
            }

            if (viewModel.PhotoScan.HasValue())
                Applicant.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                Applicant.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }
            else
            {
                Applicant.Photo = ImageManage.ResizeImageFile(_httpContextBase.Server.MapPath(DefaultAvatarPath), 150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                Applicant.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                Applicant.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }
            
            if (viewModel.CopyOfNationalCardScan.HasValue())
                Applicant.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                Applicant.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
            _Applicants.Add(Applicant);
        }
        #endregion

        #region GetPagedList
        public async Task<ApplicantListViewModel> GetPagedListAsync(ApplicantSearchRequest request)
        {
            var Applicants =
                _Applicants.Include(a => a.Creator)
                    .Include(a => a.ReferentialApplicants)
                    .Include(a => a.LasModifier)
                    .Include(a => a.Position)
                    .Include(a => a.ApproveBy)
                    .Where(
                        a =>
                            !a.ReferentialApplicants.Any(
                                r => (r.ReferencedToId == r.ReferencedFromId && !r.FinishedDate.HasValue)))
                    .AsNoTracking()
                    .AsQueryable();

            if (request.ApplicantApprovalFilter == ApplicantApprovalFilter.IsApproved)
                Applicants = Applicants.Where(a => a.IsApproved).AsQueryable();
            if (request.ApplicantApprovalFilter == ApplicantApprovalFilter.NonApproved)
                Applicants = Applicants.Where(a => !a.IsApproved).AsQueryable();
            if (request.ApplicantReferenceFilter == ApplicantReferenceFilter.Referenced)
                Applicants = Applicants.Where(a => a.IsInReference).AsQueryable();
            if (request.ApplicantReferenceFilter == ApplicantReferenceFilter.NonReferenced)
                Applicants = Applicants.Where(a => !a.IsInReference).AsQueryable();

            if (request.BirthCertificateNumber.HasValue())
                Applicants = Applicants.Where(a => a.BirthCertificateNumber == request.BirthCertificateNumber).AsQueryable();
            if (request.PersonnelCode.HasValue())
                Applicants = Applicants.Where(a => a.PersonnelCode == request.PersonnelCode).AsQueryable();
            if (request.NationalCode.HasValue())
                Applicants = Applicants.Where(a => a.NationalCode == request.NationalCode).AsQueryable();
            if (request.FirstName.HasValue())
                Applicants = Applicants.Where(a => a.FirstName.Contains(request.FirstName)).AsQueryable();
            if (request.LastName.HasValue())
                Applicants = Applicants.Where(a => a.LastName.Contains(request.LastName)).AsQueryable();

            if (request.CollegiateOrderFrom.HasValue)
                Applicants = Applicants.Where(a => a.CollegiateOrder >= request.CollegiateOrderFrom.Value).AsQueryable();
            if (request.CollegiateOrderTo.HasValue)
                Applicants = Applicants.Where(a => a.CollegiateOrder <= request.CollegiateOrderTo.Value).AsQueryable();
            if (request.OccupationalGroupFrom.HasValue)
                Applicants = Applicants.Where(a => a.OccupationalGroup >= request.OccupationalGroupFrom.Value).AsQueryable();
            if (request.OccupationalGroupTo.HasValue)
                Applicants = Applicants.Where(a => a.OccupationalGroup <= request.OccupationalGroupTo.Value).AsQueryable();

            if (request.State.HasValue())
            {
                Applicants = Applicants.Where(a => a.BirthPlaceState == request.State).AsQueryable();
                if (request.City.HasValue())
                    Applicants = Applicants.Where(a => a.BirthPlaceCity == request.City).AsQueryable();
            }
            if (request.PositionId.HasValue)
                Applicants = Applicants.Where(a => a.PositionId == request.PositionId.Value).AsQueryable();

            if (request.TrainingCenter.HasValue)
            {
                Applicants =
                    Applicants.Where(a => a.TrainingCourseId.HasValue)
                        .Select(Applicant => new { Applicant, course = Applicant.TrainingCourse })
                        .Where(@a => @a.course.TrainingCenterId == request.TrainingCenter.Value)
                        .Select(@b => @b.Applicant)
                        .AsQueryable();
            }
            Applicants = Applicants.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var selectedApplicants = Applicants.ProjectTo<ApplicantViewModel>(_mappingEngine);


            var query = await selectedApplicants
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new ApplicantListViewModel { SearchRequest = request, Applicants = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _Applicants.AnyAsync(a => a.Id == id);
        }

        public Task<bool> IsApplicantNationalCodeExist(string nationalCode, Guid? id)
        {
            nationalCode = nationalCode.GetEnglishNumber();
            return id == null
                ? (_Applicants.AnyAsync(a => a.NationalCode == nationalCode))
                : (_Applicants.AnyAsync(a => a.Id != id.Value && a.NationalCode == nationalCode));
        }

        public Task<bool> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            birthCertificateNumber = birthCertificateNumber.GetPersianNumber();
            return id == null
                ? (_Applicants.AnyAsync(a => a.BirthCertificateNumber == birthCertificateNumber))
                : (_Applicants.AnyAsync(a => a.Id != id.Value && a.BirthCertificateNumber == birthCertificateNumber));
        }

        #endregion

        #region Fill
        public async Task FillEditViewMoel(EditApplicantViewModel viewModel, string path)
        {

            viewModel.StatesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);

            viewModel.StatesForTrainingCeneter = viewModel.TrainingCenterState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.TrainingCenterState,
                    path);

            viewModel.CitiesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : viewModel.BirthPlaceCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                        viewModel.BirthPlaceCity, path);

            viewModel.CitiesForTrainingCeneter = viewModel.TrainingCenterState == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCenterCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.TrainingCenterState,
                        viewModel.TrainingCenterCity,
                        path);

            viewModel.TrainingCenters = viewModel.TrainingCenterCity == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCenterId == null
                    ? new List<SelectListItem>()
                    : await
                        _trainingCenterService.GetAsSelectListItemAsync(viewModel.TrainingCenterCity,
                            viewModel.TrainingCenterId);


            viewModel.TrainingCourses = viewModel.TrainingCenterId == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCourseId == null
                    ? new List<SelectListItem>()
                    : await
                        _trainingCourseService.GetAsSelectListByTrainingCenterIdAsync(viewModel.TrainingCenterId.Value,
                            viewModel.TrainingCourseId);

            viewModel.Positions =
                await _titleService.GetAsSelectListItemAsync(TitleType.ApplicantPosition, viewModel.PositionId);
        }

        public async Task FillAddViewMoel(AddApplicantViewModel viewModel, string path)
        {
            viewModel.StatesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);

            viewModel.StatesForTrainingCeneter = viewModel.TrainingCenterState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.TrainingCenterState,
                    path);

            viewModel.CitiesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : viewModel.BirthPlaceCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                        viewModel.BirthPlaceCity, path);

            viewModel.CitiesForTrainingCeneter = viewModel.TrainingCenterState == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCenterCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.TrainingCenterState,
                        viewModel.TrainingCenterCity,
                        path);

            viewModel.TrainingCenters = viewModel.TrainingCenterCity == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCenterId == null
                    ? new List<SelectListItem>()
                    : await
                        _trainingCenterService.GetAsSelectListItemAsync(viewModel.TrainingCenterCity,
                            viewModel.TrainingCenterId);


            viewModel.TrainingCourses = viewModel.TrainingCenterId == null
                ? new List<SelectListItem>()
                : viewModel.TrainingCourseId == null
                    ? new List<SelectListItem>()
                    : await
                        _trainingCourseService.GetAsSelectListByTrainingCenterIdAsync(viewModel.TrainingCenterId.Value,
                            viewModel.TrainingCourseId);

            viewModel.Positions =
                await _titleService.GetAsSelectListItemAsync(TitleType.ApplicantPosition, viewModel.PositionId);
        }

        public async Task<AddApplicantViewModel> GetForCreate(string path)
        {
            var viewModel = new AddApplicantViewModel
            {
                Positions = await _titleService.GetAsSelectListItemAsync(TitleType.ApplicantPosition, null),
                StatesForBirthPlace = _stateService.GetAsSelectListItemAsync(null, path),
                StatesForTrainingCeneter = _stateService.GetAsSelectListItemAsync(null, path)
            };
            return viewModel;
        }
        #endregion

        #region GetApplicantDocument
        public async Task<byte[]> GetApplicantDocument(Guid id, string type)
        {
            var file = BitConverter.GetBytes(0);
            switch (type)
            {
                case "nationalCard":
                    file = await _Applicants.Where(a => a.Id == id).Select(a => a.CopyOfNationalCard).FirstOrDefaultAsync();
                    break;
                case "birthCertificate":
                    file = await _Applicants.Where(a => a.Id == id).Select(a => a.CopyOfBirthCertificate).FirstOrDefaultAsync();
                    break;
            }

            return file;
        }
        #endregion

        #region GetApplicantDetails
        public async Task<ApplicantDetailsViewModel> GetApplicantDetails(Guid id)
        {
            var viewModel =
                await
                    _Applicants.Where(a => a.Id == id).Include(a => a.Creator)
                        .Include(a => a.LasModifier)
                        .Include(a => a.ApproveBy)
                        .Include(a => a.Position)
                        .ProjectTo<ApplicantDetailsViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync();

            if (viewModel == null) return null;
            if (!viewModel.TrainingCourseId.HasValue) return viewModel;
            var course = await _trainingCourseService.GetTrainingCourseOfApplicant(viewModel.TrainingCourseId.Value);
            viewModel.TrainingCourseDetails =
                $"{course.CourseCode}-{course.TrainingCenter.CenterName},{course.TrainingCenter.City}-{course.TrainingCenter.State}";
            return viewModel;
        }
        #endregion

        #region Approve
        public async Task<ApplicantViewModel> Approve(Guid id)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var count=await _Applicants.Where(a => a.Id == id && !a.IsInReference).UpdateAsync(a => new Applicant
            {
                ApproveById = currentUserId,
                IsApproved = true
            });
            if (count == 0) return null;
            return await GetApplicantViewModel(id);
        }

        #endregion

        #region GetApplicantViewModel
        private Task<ApplicantViewModel> GetApplicantViewModel(Guid id)
        {
            return _Applicants.Where(a => a.Id == id)
                 .Include(a => a.Creator)
                 .Include(a => a.LasModifier)
                 .Include(a => a.Position)
                 .Include(a => a.ApproveBy)
                 .AsNoTracking()
                 .ProjectTo<ApplicantViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync();
        }
        #endregion

        #region ReferApplicant
        public async Task<ApplicantViewModel> ReferApplicant(AddReferentialApplicantViewModel viewModel)
        {
            var Applicant = await _Applicants.FirstOrDefaultAsync(a => a.Id == viewModel.ApplicantId);
            if (Applicant == null) return null;
            _referentialApplicantService.Create(viewModel);
            Applicant.IsInReference = true;
            await _unitOfWork.SaveChangesAsync();
            return await GetApplicantViewModel(Applicant.Id);
        }
        #endregion

        #region CancleRefer
        public async Task<ApplicantViewModel> CancelRefer(Guid id)
        {
            var Applicant = await _Applicants.FirstOrDefaultAsync(a => a.Id == id);
            if (Applicant == null) return null;
            await _referentialApplicantService.DeleteAsync(id);
            Applicant.IsInReference = false;
            await _unitOfWork.SaveChangesAsync();
            return await GetApplicantViewModel(Applicant.Id);
        }
        #endregion

        public Task<bool> IsInRefer(Guid guid)
        {
            return _Applicants.AnyAsync(a => a.IsInReference & a.Id == guid);
        }

        public async Task<IEnumerable<ApplicantViewModel>> GetRefersApplicants()
        {
            var ApplicantIds = await _referentialApplicantService.GetRefersApplicantIds();
            return
                await _Applicants.Where(a => ApplicantIds.Any(j => j == a.Id))
                  .Include(a => a.Creator)
                 .Include(a => a.LasModifier)
                 .Include(a => a.Position)
                 .Include(a => a.ApproveBy)
                 .AsNoTracking()
                .ProjectTo<ApplicantViewModel>(_mappingEngine).ToListAsync();
        }

        public async Task<IEnumerable<ReferApplicantViewModel>> GetRefersApplicants(bool withReferer)
        {
            var currentUser = _userManager.GetCurrentUserId();
            var query = _Applicants.Include(a => a.ReferentialApplicants).Include(a => a.Position).AsNoTracking().AsQueryable();
            return withReferer
                ? await query
                    .Where(
                        a =>
                            a.ReferentialApplicants.Any(
                                b =>
                                    !b.FinishedDate.HasValue && b.ReferencedToId == currentUser &&
                                    b.ReferencedFromId != currentUser))
                    .ProjectTo<ReferApplicantViewModel>(_mappingEngine)
                    .ToListAsync()
                : await query
                    .Where(
                        a =>
                            a.ReferentialApplicants.Any(
                                b =>
                                    !b.FinishedDate.HasValue && b.ReferencedToId == currentUser &&
                                    b.ReferencedFromId == currentUser))
                    .ProjectTo<ReferApplicantViewModel>(_mappingEngine)
                    .ToListAsync();
        }

        public async Task FinishedRefer(Guid id)
        {
            var Applicant = await _Applicants.FirstAsync(a => a.Id == id);
            await _referentialApplicantService.FinishReferApplicant(id);
            Applicant.IsInReference = false;
            await _unitOfWork.SaveChangesAsync();
        }

        public long Count()
        {
            return
                _Applicants.Include(a => a.ReferentialApplicants)
                    .Where(
                        a =>
                           ! a.ReferentialApplicants.Any(
                                r => r.ReferencedFromId == r.ReferencedToId && !r.FinishedDate.HasValue))
                    .LongCount();
        }

        public long ApprovedCount()
        {
            return _Applicants.Where(a => a.IsApproved).LongCount();
        }

        public long NonApprovedCount()
        {
            return
                _Applicants.Include(a => a.ReferentialApplicants)
                    .Where(
                        a =>
                            !a.IsApproved &&
                            a.ReferentialApplicants.Any(
                                r => r.ReferencedFromId == r.ReferencedToId && r.FinishedDate.HasValue))
                    .LongCount();
        }

        public IList<ApplicantWithTopScoreViewModel> GetTenTopScoreApplicants()
        {
            return
                _Applicants.OrderByDescending(a => a.Score)
                    .Skip(0)
                    .Take(10)
                    .ProjectTo<ApplicantWithTopScoreViewModel>(_mappingEngine)
                    .ToList();
        }

        public IList<NewAddedApplicantViewModel> GetTenNewAddedApplicants()
        {
            return
               _Applicants.OrderByDescending(a => a.CreateDate)
                   .Skip(0)
                   .Take(10)
                   .ProjectTo<NewAddedApplicantViewModel>(_mappingEngine)
                   .ToList();
        }
    }
}
