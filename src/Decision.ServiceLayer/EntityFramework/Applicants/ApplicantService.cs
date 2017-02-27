using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Decision.DomainClasses.Applicants;
using Decision.Framework.Domain.Services;
using Decision.Framework.Domain.Uow;
using Decision.ServiceLayer.Interfaces.Applicants;
using Decision.ServiceLayer.Interfaces.Common;
using Decision.ServiceLayer.Interfaces.Identity;
using Decision.ViewModels.GeneralBasicData.Applicants;

namespace Decision.ServiceLayer.EntityFramework.Applicants
{
    public class ApplicantService :
        ServiceBase
            <Applicant, long, ApplicantViewModel, CreateApplicantViewModel, EditApplicantViewModel,
                ApplicantListViewModel, ApplicantListRequest>, IApplicantService
    {
        #region Constructor
        public ApplicantService(HttpContextBase httpContextBase,
            IUnitOfWork unitOfWork,
            IApplicationUserManager userManager,
            IMapper mapper,
            IStateService stateService,
            ICityService cityService
            ) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _applicants = _unitOfWork.Set<Applicant>();

            _cityService = cityService;
            _stateService = stateService;
            _httpContextBase = httpContextBase;
        }

        #endregion

        #region Fields

        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly IDbSet<Applicant> _applicants;
        private readonly HttpContextBase _httpContextBase;

        #endregion

        #region Public Methods

        public bool CheckFirstNameExist(string firstName, long? id)
        {
            throw new NotImplementedException();
        }

        public bool CheckLastNameExist(string lastName, long? id)
        {
            throw new NotImplementedException();
        }

        public bool CheckEmailAddressExist(string emailAddress, long? id)
        {
            throw new NotImplementedException();
        }

        public bool CheckNationalCodeExist(string nationalCode, long? id)
        {
            throw new NotImplementedException();
        }

        public bool CheckBirthCertificateNumberExist(string birthCertificateNumber, long? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckFirstNameExistAsync(string firstName, long? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckLastNameExistAsync(string lastName, long? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckEmailAddressExistAsync(string emailAddress, long? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckNationalCodeExistAsync(string nationalCode, long? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckBirthCertificateNumberExistAsync(string birthCertificateNumber, long? id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}