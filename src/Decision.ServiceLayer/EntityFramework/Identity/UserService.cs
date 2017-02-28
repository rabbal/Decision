using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using Decision.DomainClasses.Identity;
using Decision.Framework.Configuration;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Uow;
using Decision.Framework.IdentityToolkit.Validators;
using Decision.Framework.Net.Mail.Postal;
using Decision.Framework.Utility;
using Decision.ServiceLayer.Interfaces.Identity;
using Decision.ViewModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class UserService : UserManager<User, long>, IUserService
    {
        #region Constructor

        public UserService(
            IEmailService postEmailService,
            IAppConfiguration configuration,
            HttpContextBase httpContext,
            IUserStore<User, long> store,
            ICurrentUser currentUser,
            IDataProtectionProvider dataProtectionProvider,
            IUnitOfWork unitOfWork,
            IRoleService roleManager,
            IIdentityMessageService smsService,
            IIdentityMessageService emailService,

            IMapper mapper)
            : base(store)
        {
            _postEmailService = postEmailService;
            _configuration = configuration;
            _httpContext = httpContext;
            _currentUser = currentUser;
            _roleService = roleManager;
            _dataProtectionProvider = dataProtectionProvider;
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
            EmailService = emailService;
            SmsService = smsService;
            CreateApplicationManager();
            _mapper = mapper;
        }

        #endregion

        #region Fields

        private readonly ICurrentUser _currentUser;
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMapper _mapper;
        private readonly IEmailService _postEmailService;
        private readonly IAppConfiguration _configuration;
        private readonly HttpContextBase _httpContext;

        #endregion

        #region validation Methods
        public bool IsDisposableEmail(string email)
        {
            //var result = _emailDetector.IsDisposableEmailAddress(email.FixGmailDots(),
            //   _configuration.NameApiKey);
            //return result;
            return false;
        }

        public bool CheckUserNameExist(string userName, long? id)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            userName = userName.ToLowerInvariant();

            return id.HasValue
                ? _users.Any(a => a.UserName == userName && a.Id != id.Value)
                : _users.Any(a => a.UserName == userName);
        }

        public bool CheckEmailExist(string email, long? id)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            email = email.FixGmailDots();
            return id.HasValue
                ? _users.Any(a => a.Email == email && a.Id != id.Value)
                : _users.Any(a => a.Email == email);
        }


        public bool CheckPhoneNumberExist(string phoneNumber, long? id)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));

            return id.HasValue
                ? _users.Any(a => a.PhoneNumber == phoneNumber && a.Id != id.Value)
                : _users.Any(a => a.PhoneNumber == phoneNumber);
        }

        public Task<bool> IsDisposableEmailAsync(string email)
        {
            //    var result = _emailDetector.IsDisposableEmailAddressAsync(email.FixGmailDots(),
            //         _configuration.NameApiKey);
            //    return result;
            return Task.FromResult(false);
        }

        public Task<bool> CheckUserNameExistAsync(string userName, long? id)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            userName = userName.ToLowerInvariant();

            return id.HasValue
                ? _users.AnyAsync(
                    a =>
                        a.UserName == userName &&
                        a.Id != id.Value)
                : _users.AnyAsync(a => a.UserName == userName);
        }

        public Task<bool> CheckEmailExistAsync(string email, long? id)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            email = email.FixGmailDots();
            return id.HasValue
                ? _users.AnyAsync(a => a.Email == email && a.Id != id.Value)
                : _users.AnyAsync(a => a.Email == email);
        }



        public Task<bool> CheckPhoneNumberExistAsync(string phoneNumber, long? id)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));

            return id.HasValue
                ? _users.AnyAsync(a => a.PhoneNumber == phoneNumber && a.Id != id.Value)
                : _users.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        #endregion

        #region Public Methods
        public void SeedDatabase()
        {
            const string displayName = "مدیر سیستم";
            const string userName = "Admin";
            const string email = "Admin@gmail.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            var role = _roleService.FindRoleByName(roleName);
            if (role == null)
            {
                role = new Role { Name = roleName, IsSystemEntry = true };
                _roleService.CreateRole(role);
            }

            var user = this.FindByName(userName);
            if (user == null)
            {
                user = new User { UserName = userName, Email = email, FirstName = displayName };
                this.Create(user, password);
                this.SetLockoutEnabled(user.Id, false);
            }

            var rolesForUser = this.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                this.AddToRole(user.Id, role.Name);
            }
        }
        public SelectListItem[] GetAvailableUsers()
        {
            throw new NotImplementedException();
        }
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return SecurityStampValidator.OnValidateIdentity<UserService, User, long>(
                TimeSpan.FromMinutes(5),
                GenerateUserIdentityAsync,
                identity => _currentUser.Id
                );
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity =
                await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            // Add custom user claims here

            return userIdentity;
        }
        public IUserEmailStore<User, long> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, long>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }
        public IList<string> GetRoleNames(long id)
        {
            return this.GetRoles(id);
        }
        public string GetSerialNumber(long userId)
        {
            return this.GetSecurityStamp(userId);
        }
        public Task<bool> CheckIsBannedAsync(string userName)
        {
            userName = userName.ToLowerInvariant();
            return _users.AnyAsync(a => a.UserName == userName && !a.IsActive);
        }
        public bool CheckIsBanned(string userName)
        {
            userName = userName.ToLowerInvariant();
            return _users.Any(a => a.UserName == userName && !a.IsActive);
        }
        public void Create(AddUserViewModel viewModel)
        {
            throw new NotImplementedException();
        }
        public Task CreateAsync(AddUserViewModel viewModel)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Private Methods
        private static async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserService manager, User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity =
                await
                    manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
                        .ConfigureAwait(false);
            // Add custom user claims here

            return userIdentity;
        }
        private void CreateApplicationManager()
        {
            UserValidator = new NullUserValidator<User, long>();
            PasswordValidator = new NullPasswordValidator();

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User, long>
            {
                MessageFormat = "{0} کد امنیتی درخواست شده توسط شما:"
            });

            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User, long>
            {
                Subject = "کد امنیتی درخواست شده توسط شما",
                BodyFormat = "{0} کد امنیتی:"
            });

            if (_dataProtectionProvider == null) return;

            var dataProtector = _dataProtectionProvider.Create("Asp.net Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtector)
            {
                TokenLifespan = TimeSpan.FromHours(24)
            };
        }

        #endregion
    }
}