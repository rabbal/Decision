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
using Decision.Common.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Home;
using Decision.ViewModel.Applicant;
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

        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Applicant> _applicants;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor

        public ApplicantService(HttpContextBase httpContextBase, IUnitOfWork unitOfWork, IApplicationUserManager userManager,
            IMappingEngine mappingEngine, IStateService stateService, ICityService cityService
            )
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _applicants = _unitOfWork.Set<Applicant>();
            _mappingEngine = mappingEngine;
            _cityService = cityService;
            _stateService = stateService;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _applicants.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region GetForEdit
        public async Task<EditApplicantViewModel> GetForEditAsync(Guid id, string path)
        {
            var viewModel =
                await
                    _applicants.AsNoTracking()
                        .ProjectTo<EditApplicantViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync(a => a.Id == id);

            if (viewModel == null) return null;

            viewModel.StatesForBirthPlace = _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);
            viewModel.CitiesForBirthPlace = _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                viewModel.BirthPlaceCity, path);
            return viewModel;
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditApplicantViewModel viewModel)
        {
            var applicant = await _applicants.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, applicant);

            if (viewModel.PhotoScan.HasValue())
                applicant.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                applicant.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                applicant.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                applicant.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }
            
            if (viewModel.CopyOfNationalCardScan.HasValue())
                applicant.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                applicant.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
        }
        #endregion

        #region Create
        public void Create(AddApplicantViewModel viewModel)
        {
            var applicant = _mappingEngine.Map<Applicant>(viewModel);

            if (viewModel.PhotoScan.HasValue())
                applicant.Photo = Convert.FromBase64String(viewModel.PhotoScan).ResizeImageFile(150, 150);
            else if (viewModel.PhotoFile.HasFile())
            {
                applicant.Photo = viewModel.PhotoFile.InputStream.ResizeImageFile(150, 150);
            }
            else
            {
                applicant.Photo = ImageManage.ResizeImageFile(_httpContextBase.Server.MapPath(DefaultAvatarPath), 150, 150);
            }

            if (viewModel.CopyOfBirthCertificateScan.HasValue())
                applicant.CopyOfBirthCertificate = Convert.FromBase64String(viewModel.CopyOfBirthCertificateScan).ResizeImageFile(A5Width, A5Height);
            else if (viewModel.CopyOfBirthCertificateFile.HasFile())
            {
                applicant.CopyOfBirthCertificate = viewModel.CopyOfBirthCertificateFile.InputStream.ResizeImageFile(A5Width, A5Height);
            }

            if (viewModel.CopyOfNationalCardScan.HasValue())
                applicant.CopyOfNationalCard = Convert.FromBase64String(viewModel.CopyOfNationalCardScan).ResizeImageFile(A6Width, A6Height);
            else if (viewModel.CopyOfNationalCardFile.HasFile())
            {
                applicant.CopyOfNationalCard = viewModel.CopyOfNationalCardFile.InputStream.ResizeImageFile(A6Width, A6Height);
            }
            _applicants.Add(applicant);
        }
        #endregion

        #region GetPagedList
        public async Task<ApplicantListViewModel> GetPagedListAsync(ApplicantSearchRequest request)
        {
            var applicants =
                _applicants.Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .AsNoTracking()
                    .AsQueryable();

            if (request.BirthCertificateNumber.HasValue())
                applicants = applicants.Where(a => a.BirthCertificateNumber == request.BirthCertificateNumber).AsQueryable();
            if (request.NationalCode.HasValue())
                applicants = applicants.Where(a => a.NationalCode == request.NationalCode).AsQueryable();
            if (request.FirstName.HasValue())
                applicants = applicants.Where(a => a.FirstName.Contains(request.FirstName)).AsQueryable();
            if (request.LastName.HasValue())
                applicants = applicants.Where(a => a.LastName.Contains(request.LastName)).AsQueryable();

            if (request.State.HasValue())
            {
                applicants = applicants.Where(a => a.BirthPlaceState == request.State).AsQueryable();
                if (request.City.HasValue())
                    applicants = applicants.Where(a => a.BirthPlaceCity == request.City).AsQueryable();
            }
            applicants = applicants.OrderBy($"{request.CurrentSort} {request.SortDirection}");

            var selectedApplicants = applicants.ProjectTo<ApplicantViewModel>(_mappingEngine);
            var resultsToSkip = (request.PageIndex - 1)*request.PageSize;
            var query = await selectedApplicants
                .Skip(() => resultsToSkip)
                .Take(() => request.PageSize)
                .ToListAsync();

            return new ApplicantListViewModel { SearchRequest = request, Applicants = query };
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _applicants.AnyAsync(a => a.Id == id);
        }

        public Task<bool> IsApplicantNationalCodeExist(string nationalCode, Guid? id)
        {
            nationalCode = nationalCode.GetEnglishNumber();
            return id == null
                ? (_applicants.AnyAsync(a => a.NationalCode == nationalCode))
                : (_applicants.AnyAsync(a => a.Id != id.Value && a.NationalCode == nationalCode));
        }

        public Task<bool> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            birthCertificateNumber = birthCertificateNumber.GetPersianNumber();
            return id == null
                ? (_applicants.AnyAsync(a => a.BirthCertificateNumber == birthCertificateNumber))
                : (_applicants.AnyAsync(a => a.Id != id.Value && a.BirthCertificateNumber == birthCertificateNumber));
        }

        #endregion

        #region Fill
        public async Task FillEditViewMoel(EditApplicantViewModel viewModel, string path)
        {

            viewModel.StatesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);


            viewModel.CitiesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : viewModel.BirthPlaceCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                        viewModel.BirthPlaceCity, path);

        }

        public async Task FillAddViewMoel(AddApplicantViewModel viewModel, string path)
        {
            viewModel.StatesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : _stateService.GetAsSelectListItemAsync(viewModel.BirthPlaceState, path);


            viewModel.CitiesForBirthPlace = viewModel.BirthPlaceState == null
                ? new List<SelectListItem>()
                : viewModel.BirthPlaceCity == null
                    ? new List<SelectListItem>()
                    : _cityService.GetAsSelectListByStateNameAsync(viewModel.BirthPlaceState,
                        viewModel.BirthPlaceCity, path);
        }

        public async Task<AddApplicantViewModel> GetForCreate(string path)
        {
            var viewModel = new AddApplicantViewModel
            {
                StatesForBirthPlace = _stateService.GetAsSelectListItemAsync(null, path)
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
                    file = await _applicants.Where(a => a.Id == id).Select(a => a.CopyOfNationalCard).FirstOrDefaultAsync();
                    break;
                case "birthCertificate":
                    file = await _applicants.Where(a => a.Id == id).Select(a => a.CopyOfBirthCertificate).FirstOrDefaultAsync();
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
                    _applicants.Where(a => a.Id == id).Include(a => a.CreatedBy)
                        .Include(a => a.ModifiedBy)
                        .ProjectTo<ApplicantDetailsViewModel>(_mappingEngine)
                        .FirstOrDefaultAsync();
            return viewModel;
        }
        #endregion

        #region Approve
        public async Task<ApplicantViewModel> Approve(Guid id)
        {
            var currentUserId = _userManager.GetCurrentUserId();
            var count = await _applicants.Where(a => a.Id == id).UpdateAsync(a => new Applicant
            {
                Status = ApplicantStatus.Approved
            });
            if (count == 0) return null;
            return await GetApplicantViewModel(id);
        }

        #endregion

        #region GetApplicantViewModel
        private Task<ApplicantViewModel> GetApplicantViewModel(Guid id)
        {
            return _applicants.Where(a => a.Id == id)
                 .Include(a => a.CreatedBy)
                 .Include(a => a.ModifiedBy)
                 .AsNoTracking()
                 .ProjectTo<ApplicantViewModel>(_mappingEngine)
                 .FirstOrDefaultAsync();
        }
        #endregion
    }
}
