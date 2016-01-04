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
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Home;
using Decision.ViewModel.Teacher;
using Decision.ViewModel.ReferentialTeacher;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.TeacherInfo
{
    /// <summary>
    /// نشان دهنده سرویس دهنده عملیات مرتبط با استاد
    /// </summary>
    public class TeacherService : ITeacherService
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
        private readonly IReferentialTeacherService _referentialTeacherService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Teacher> _Teachers;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor

        public TeacherService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager, ITrainingCenterService trainingCenter, ITrainingCourseService trainingCourse,
            IMappingEngine mappingEngine, IReferentialTeacherService referentialTeacherService, ITitleService titleService, IStateService stateService, ICityService cityService
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _Teachers = _unitOfWork.Set<Teacher>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
            _titleService = titleService;
            _httpContextBase = httpContextBase;
            _referentialTeacherService = referentialTeacherService;
            _trainingCenterService = trainingCenter;
            _trainingCourseService = trainingCourse;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _Teachers.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async Task<EditTeacherViewModel> GetForEditAsync(Guid id, string path)
        {
            var viewModel =
                await
                    _Teachers.AsNoTracking()
                        .ProjectTo<EditTeacherViewModel>(_mappingEngine)
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
                await _titleService.GetAsSelectListItemAsync(TitleType.TeacherPosition, viewModel.PositionId);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditTeacherViewModel viewModel)
        {
            var Teacher = await _Teachers.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, Teacher);

            if (viewModel.PhotoScan.HasValue())
                Teacher.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                Teacher.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                Teacher.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                Teacher.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }


            if (viewModel.CopyOfNationalCardScan.HasValue())
                Teacher.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                Teacher.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
            Teacher.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create
        public void Create(AddTeacherViewModel viewModel)
        {
            var Teacher = _mappingEngine.Map<Teacher>(viewModel);
            Teacher.CreatorId = _userManager.GetCurrentUserId();

            if (_userManager.IsAdministrator())
            {
                Teacher.IsInReference = false;
                Teacher.IsApproved = true;
                Teacher.ApproveById = Teacher.CreatorId;
            }
            else if (_userManager.IsOperator())
            {
                _referentialTeacherService.Create(new AddReferentialTeacherViewModel
                {
                    TeacherId = Teacher.Id,
                    ReferencedToId = Teacher.CreatorId
                });
            }

            if (viewModel.PhotoScan.HasValue())
                Teacher.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                Teacher.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }
            else
            {
                Teacher.Photo = ImageManage.ResizeImageFile(_httpContextBase.Server.MapPath(DefaultAvatarPath), 150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                Teacher.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                Teacher.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }
            
            if (viewModel.CopyOfNationalCardScan.HasValue())
                Teacher.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                Teacher.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
            _Teachers.Add(Teacher);
        }
        #endregion

        #region GetPagedList
        public async Task<TeacherListViewModel> GetPagedListAsync(TeacherSearchRequest request)
        {
            var Teachers =
                _Teachers.Include(a => a.Creator)
                    .Include(a => a.ReferentialTeachers)
                    .Include(a => a.LasModifier)
                    .Include(a => a.Position)
                    .Include(a => a.ApproveBy)
                    .Where(
                        a =>
                            !a.ReferentialTeachers.Any(
                                r => (r.ReferencedToId == r.ReferencedFromId && !r.FinishedDate.HasValue)))
                    .AsNoTracking()
                    .AsQueryable();

            if (request.TeacherApprovalFilter == TeacherApprovalFilter.IsApproved)
                Teachers = Teachers.Where(a => a.IsApproved).AsQueryable();
            if (request.TeacherApprovalFilter == TeacherApprovalFilter.NonApproved)
                Teachers = Teachers.Where(a => !a.IsApproved).AsQueryable();
            if (request.TeacherReferenceFilter == TeacherReferenceFilter.Referenced)
                Teachers = Teachers.Where(a => a.IsInReference).AsQueryable();
            if (request.TeacherReferenceFilter == TeacherReferenceFilter.NonReferenced)
                Teachers = Teachers.Where(a => !a.IsInReference).AsQueryable();

            if (request.BirthCertificateNumber.HasValue())
                Teachers = Teachers.Where(a => a.BirthCertificateNumber == request.BirthCertificateNumber).AsQueryable();
            if (request.PersonnelCode.HasValue())
                Teachers = Teachers.Where(a => a.PersonnelCode == request.PersonnelCode).AsQueryable();
            if (request.NationalCode.HasValue())
                Teachers = Teachers.Where(a => a.NationalCode == request.NationalCode).AsQueryable();
            if (request.FirstName.HasValue())
                Teachers = Teachers.Where(a => a.FirstName.Contains(request.FirstName)).AsQueryable();
            if (request.LastName.HasValue())
                Teachers = Teachers.Where(a => a.LastName.Contains(request.LastName)).AsQueryable();

            if (request.CollegiateOrderFrom.HasValue)
                Teachers = Teachers.Where(a => a.CollegiateOrder >= request.CollegiateOrderFrom.Value).AsQueryable();
            if (request.CollegiateOrderTo.HasValue)
                Teachers = Teachers.Where(a => a.CollegiateOrder <= request.CollegiateOrderTo.Value).AsQueryable();
            if (request.OccupationalGroupFrom.HasValue)
                Teachers = Teachers.Where(a => a.OccupationalGroup >= request.OccupationalGroupFrom.Value).AsQueryable();
            if (request.OccupationalGroupTo.HasValue)
                Teachers = Teachers.Where(a => a.OccupationalGroup <= request.OccupationalGroupTo.Value).AsQueryable();

            if (request.State.HasValue())
            {
                Teachers = Teachers.Where(a => a.BirthPlaceState == request.State).AsQueryable();
                if (request.City.HasValue())
                    Teachers = Teachers.Where(a => a.BirthPlaceCity == request.City).AsQueryable();
            }
            if (request.PositionId.HasValue)
                Teachers = Teachers.Where(a => a.PositionId == request.PositionId.Value).AsQueryable();

            if (request.TrainingCenter.HasValue)
            {
                Teachers =
                    Teachers.Where(a => a.TrainingCourseId.HasValue)
                        .Select(Teacher => new { Teacher, course = Teacher.TrainingCourse })
                        .Where(@a => @a.course.TrainingCenterId == request.TrainingCenter.Value)
                        .Select(@b => @b.Teacher)
                        .AsQueryable();
            }
            Teachers = Teachers.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var selectedTeachers = Teachers.ProjectTo<TeacherViewModel>(_mappingEngine);


            var query = await selectedTeachers
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new TeacherListViewModel { SearchRequest = request, Teachers = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _Teachers.AnyAsync(a => a.Id == id);
        }

        public Task<bool> IsTeacherNationalCodeExist(string nationalCode, Guid? id)
        {
            nationalCode = nationalCode.GetEnglishNumber();
            return id == null
                ? (_Teachers.AnyAsync(a => a.NationalCode == nationalCode))
                : (_Teachers.AnyAsync(a => a.Id != id.Value && a.NationalCode == nationalCode));
        }

        public Task<bool> IsTeacherBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            birthCertificateNumber = birthCertificateNumber.GetPersianNumber();
            return id == null
                ? (_Teachers.AnyAsync(a => a.BirthCertificateNumber == birthCertificateNumber))
                : (_Teachers.AnyAsync(a => a.Id != id.Value && a.BirthCertificateNumber == birthCertificateNumber));
        }

        #endregion

        #region Fill
        public async Task FillEditViewMoel(EditTeacherViewModel viewModel, string path)
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
                await _titleService.GetAsSelectListItemAsync(TitleType.TeacherPosition, viewModel.PositionId);
        }

        public async Task FillAddViewMoel(AddTeacherViewModel viewModel, string path)
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
                await _titleService.GetAsSelectListItemAsync(TitleType.TeacherPosition, viewModel.PositionId);
        }

        public async Task<AddTeacherViewModel> GetForCreate(string path)
        {
            var viewModel = new AddTeacherViewModel
            {
                Positions = await _titleService.GetAsSelectListItemAsync(TitleType.TeacherPosition, null),
                StatesForBirthPlace = _stateService.GetAsSelectListItemAsync(null, path),
                StatesForTrainingCeneter = _stateService.GetAsSelectListItemAsync(null, path)
            };
            return viewModel;
        }
        #endregion

        #region GetTeacherDocument
        public async Task<byte[]> GetTeacherDocument(Guid id, string type)
        {
            var file = BitConverter.GetBytes(0);
            switch (type)
            {
                case "nationalCard":
                    file = await _Teachers.Where(a => a.Id == id).Select(a => a.CopyOfNationalCard).FirstOrDefaultAsync();
                    break;
                case "birthCertificate":
                    file = await _Teachers.Where(a => a.Id == id).Select(a => a.CopyOfBirthCertificate).FirstOrDefaultAsync();
                    break;
            }

            return file;
        }
        #endregion

        #region GetTeacherDetails
        public async Task<TeacherDetailsViewModel> GetTeacherDetails(Guid id)
        {
            var viewModel =
                await
                    _Teachers.Where(a => a.Id == id).Include(a => a.Creator)
                        .Include(a => a.LasModifier)
                        .Include(a => a.ApproveBy)
                        .Include(a => a.Position)
                        .ProjectTo<TeacherDetailsViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync();

            if (viewModel == null) return null;
            if (!viewModel.TrainingCourseId.HasValue) return viewModel;
            var course = await _trainingCourseService.GetTrainingCourseOfTeacher(viewModel.TrainingCourseId.Value);
            viewModel.TrainingCourseDetails =
                $"{course.CourseCode}-{course.TrainingCenter.CenterName},{course.TrainingCenter.City}-{course.TrainingCenter.State}";
            return viewModel;
        }
        #endregion

        #region Approve
        public async Task<TeacherViewModel> Approve(Guid id)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var count=await _Teachers.Where(a => a.Id == id && !a.IsInReference).UpdateAsync(a => new Teacher
            {
                ApproveById = currentUserId,
                IsApproved = true
            });
            if (count == 0) return null;
            return await GetTeacherViewModel(id);
        }

        #endregion

        #region GetTeacherViewModel
        private Task<TeacherViewModel> GetTeacherViewModel(Guid id)
        {
            return _Teachers.Where(a => a.Id == id)
                 .Include(a => a.Creator)
                 .Include(a => a.LasModifier)
                 .Include(a => a.Position)
                 .Include(a => a.ApproveBy)
                 .AsNoTracking()
                 .ProjectTo<TeacherViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync();
        }
        #endregion

        #region ReferTeacher
        public async Task<TeacherViewModel> ReferTeacher(AddReferentialTeacherViewModel viewModel)
        {
            var Teacher = await _Teachers.FirstOrDefaultAsync(a => a.Id == viewModel.TeacherId);
            if (Teacher == null) return null;
            _referentialTeacherService.Create(viewModel);
            Teacher.IsInReference = true;
            await _unitOfWork.SaveChangesAsync();
            return await GetTeacherViewModel(Teacher.Id);
        }
        #endregion

        #region CancleRefer
        public async Task<TeacherViewModel> CancelRefer(Guid id)
        {
            var Teacher = await _Teachers.FirstOrDefaultAsync(a => a.Id == id);
            if (Teacher == null) return null;
            await _referentialTeacherService.DeleteAsync(id);
            Teacher.IsInReference = false;
            await _unitOfWork.SaveChangesAsync();
            return await GetTeacherViewModel(Teacher.Id);
        }
        #endregion

        public Task<bool> IsInRefer(Guid guid)
        {
            return _Teachers.AnyAsync(a => a.IsInReference & a.Id == guid);
        }

        public async Task<IEnumerable<TeacherViewModel>> GetRefersTeachers()
        {
            var TeacherIds = await _referentialTeacherService.GetRefersTeacherIds();
            return
                await _Teachers.Where(a => TeacherIds.Any(j => j == a.Id))
                  .Include(a => a.Creator)
                 .Include(a => a.LasModifier)
                 .Include(a => a.Position)
                 .Include(a => a.ApproveBy)
                 .AsNoTracking()
                .ProjectTo<TeacherViewModel>(_mappingEngine).ToListAsync();
        }

        public async Task<IEnumerable<ReferTeacherViewModel>> GetRefersTeachers(bool withReferer)
        {
            var currentUser = _userManager.GetCurrentUserId();
            var query = _Teachers.Include(a => a.ReferentialTeachers).Include(a => a.Position).AsNoTracking().AsQueryable();
            return withReferer
                ? await query
                    .Where(
                        a =>
                            a.ReferentialTeachers.Any(
                                b =>
                                    !b.FinishedDate.HasValue && b.ReferencedToId == currentUser &&
                                    b.ReferencedFromId != currentUser))
                    .ProjectTo<ReferTeacherViewModel>(_mappingEngine)
                    .ToListAsync()
                : await query
                    .Where(
                        a =>
                            a.ReferentialTeachers.Any(
                                b =>
                                    !b.FinishedDate.HasValue && b.ReferencedToId == currentUser &&
                                    b.ReferencedFromId == currentUser))
                    .ProjectTo<ReferTeacherViewModel>(_mappingEngine)
                    .ToListAsync();
        }

        public async Task FinishedRefer(Guid id)
        {
            var Teacher = await _Teachers.FirstAsync(a => a.Id == id);
            await _referentialTeacherService.FinishReferTeacher(id);
            Teacher.IsInReference = false;
            await _unitOfWork.SaveChangesAsync();
        }

        public long Count()
        {
            return
                _Teachers.Include(a => a.ReferentialTeachers)
                    .Where(
                        a =>
                           ! a.ReferentialTeachers.Any(
                                r => r.ReferencedFromId == r.ReferencedToId && !r.FinishedDate.HasValue))
                    .LongCount();
        }

        public long ApprovedCount()
        {
            return _Teachers.Where(a => a.IsApproved).LongCount();
        }

        public long NonApprovedCount()
        {
            return
                _Teachers.Include(a => a.ReferentialTeachers)
                    .Where(
                        a =>
                            !a.IsApproved &&
                            a.ReferentialTeachers.Any(
                                r => r.ReferencedFromId == r.ReferencedToId && r.FinishedDate.HasValue))
                    .LongCount();
        }

        public IList<TeacherWithTopScoreViewModel> GetTenTopScoreTeachers()
        {
            return
                _Teachers.OrderByDescending(a => a.Score)
                    .Skip(0)
                    .Take(10)
                    .ProjectTo<TeacherWithTopScoreViewModel>(_mappingEngine)
                    .ToList();
        }

        public IList<NewAddedTeacherViewModel> GetTenNewAddedTeachers()
        {
            return
               _Teachers.OrderByDescending(a => a.CreateDate)
                   .Skip(0)
                   .Take(10)
                   .ProjectTo<NewAddedTeacherViewModel>(_mappingEngine)
                   .ToList();
        }
    }
}
