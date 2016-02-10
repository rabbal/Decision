using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Controller;
using Decision.Common.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.CustomAspNetIdentity;
using Decision.ServiceLayer.Security;
using Decision.Utility;
using Decision.ViewModel.User;
using EFSecondLevelCache;
using EFSecondLevelCache.Contracts;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using RefactorThis.GraphDiff;

namespace Decision.ServiceLayer.EFServiecs.Users
{
    public class ApplicationUserManager : UserManager<User, Guid>, IApplicationUserManager
    {

        #region Fields

        private const string aspNetIdentityRequiredEmail = "email@example.com";
        private readonly IIdentity _identity;
        private User _user;
        private readonly IPermissionService _permissionService;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMappingEngine _mappingEngine;

        #endregion

        #region Constructor

        public ApplicationUserManager(
            IPermissionService permissionService, IUserStore<User, Guid> userStore,
            IApplicationRoleManager roleManager, IUnitOfWork unitOfWork, IIdentity identity,
            IMappingEngine mappingEngine, IDataProtectionProvider dataProtectionProvider,
             IIdentityMessageService smsService,
            IIdentityMessageService emailService)
            : base(userStore)
        {
            _permissionService = permissionService;
            AutoCommitEnabled = true;
            _dataProtectionProvider = dataProtectionProvider;
            _mappingEngine = mappingEngine;
            EmailService = emailService;
            SmsService = smsService;
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
            _roleManager = roleManager;
            _identity = identity;
            CreateApplicationUserManager();
        }

        #endregion

        #region GenerateUserIdentityAsync
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        #endregion

        #region HasPassword

        public async Task<bool> HasPassword(Guid userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        #endregion

        #region HasPhoneNumber
        public async Task<bool> HasPhoneNumber(Guid userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }
        #endregion

        #region OnValidateIdentity
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return CustomSecurityStampValidator.OnValidateIdentity(
                         validateInterval: TimeSpan.FromMinutes(0),
                         regenerateIdentityCallback: GenerateUserIdentityAsync,
                         getUserIdCallback: identity => Guid.Parse(identity.GetUserId()));
        }

        #endregion

        #region SeedDatabase
        public void SeedDatabase()
        {
            const string systemAdminUserName = "Admin";
            const string systemAdminPassword = "Admin1234@example.com";
            const string systemAdminEmail = "Admin@gmail.com";
            const string systemAdminDisplayName = "مدیر سیستم";

            var user = _users.FirstOrDefault(a => a.IsSystemAccount);
            if (user == null)
            {
                user = new User
                {
                    DisplayName = systemAdminDisplayName,
                    UserName = systemAdminUserName,
                    IsSystemAccount = true,
                    Email = systemAdminEmail.FixGmailDots(),
                    IsApproved = true

                };
                this.Create(user, systemAdminPassword);
                this.SetLockoutEnabled(user.Id, false);
            }

            var userRoles = _roleManager.FindUserRoles(user.Id);
            if (userRoles.Any(a => a == StandardRoles.Administrators)) return;
            this.AddToRole(user.Id, StandardRoles.Administrators);
        }

        #endregion

        #region GenerateUserIdentityAsync
        private static async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        #endregion

        #region GetAllUsersAsync
        public Task<List<User>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }
        #endregion

        #region CreateApplicationUserManager

        private void CreateApplicationUserManager()
        {
            ClaimsIdentityFactory = new CustomClaimsIdentityFactory();

            UserValidator = new CustomUserValidator<User, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false

            };

            PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            if (_dataProtectionProvider == null) return;

            var dataProtector = _dataProtectionProvider.Create("Asp.net Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtector);

        }
        #endregion

        #region DeleteAll
        public async Task RemoveAll()
        {
            await Users.DeleteAsync();
        }
        #endregion

        #region GetUsersWithRoleIds
        public IList<User> GetUsersWithRoleIds(params Guid[] ids)
        {
            return Users.Where(applicationUser => ids.Contains(applicationUser.Id))
                .ToList();
        }
        #endregion

        #region AutoCommitEnabled
        public bool AutoCommitEnabled { get; set; }

        #endregion

        #region Any
        public bool Any()
        {
            return _users.Any();
        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<User> users)
        {
            _unitOfWork.AddThisRange(users);
        }
        #endregion

        #region GetPageList
        public async Task<UserListViewModel> GetPageList(UserSearchRequest search)
        {
            var users = _users.AsNoTracking().OrderBy(a => a.UserName).AsQueryable();

            if (search.RoleId.HasValue)
            {
                users =
                    users.Include(a => a.Roles)
                        .Where(a => a.Roles.Any(r => r.RoleId == search.RoleId.Value))
                        .AsQueryable();
            }

            if (search.UserName.HasValue())
                users = users.Where(a => a.UserName.Contains(search.UserName));

            var query = await users
                    .Skip((search.PageIndex - 1) * 10).Take(10).ProjectTo<UserViewModel>(_mappingEngine)
                    .ToListAsync();

            return new UserListViewModel
            {
                SearchRequest = search,
                Users = query
            };
        }
        #endregion

        #region GetForEditAsync
        public async Task<EditUserViewModel> GetForEditAsync(Guid id)
        {
            var userWithRoles = await
                 _users.AsNoTracking()
                     .Include(a => a.Roles)
                     .FirstOrDefaultAsync(a => a.Id == id);
            if (userWithRoles == null) return null;
            var viewModel = _mappingEngine.Map<EditUserViewModel>(userWithRoles);
            viewModel.Roles = await _roleManager.GetAllAsSelectList();
            if (userWithRoles.Roles!=null  && userWithRoles.Roles.Any())
                viewModel.Roles.ToList().ForEach(a => a.Selected = userWithRoles.Roles.First().RoleId.ToString() == a.Value);
            return viewModel;
        }

        #endregion

        #region EditUser
        public async Task<UserViewModel> EditUser(EditUserViewModel viewModel)
        {
            var user = _users.Find(viewModel.Id);
            _mappingEngine.Map(viewModel, user);
            if (viewModel.Password.IsNotEmpty())
            {
                user.PasswordHash = PasswordHasher.HashPassword(viewModel.Password);
            }

            if (viewModel.RoleId.HasValue)
            {
                _unitOfWork.MarkAsDetached(user);
                user.Roles.Add(new UserRole { RoleId = viewModel.RoleId.Value, UserId = user.Id });
                _unitOfWork.Update(user, a => a.AssociatedCollection(u => u.Roles));
            }
            {
                user.Roles.Clear();
            }

            if (!await IsInRoleAsync(user.Id, StandardRoles.Administrators))
                this.UpdateSecurityStamp(user.Id);
            else await _unitOfWork.SaveAllChangesAsync();

            return await GetUserViewModel(viewModel.Id);
        }
        #endregion

        #region SetRolesToUser
        public void SetRolesToUser(User user, IEnumerable<Role> roles)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddUser
        public async Task<UserViewModel> AddUser(AddUserViewModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            user.Email = aspNetIdentityRequiredEmail;
            await CreateAsync(user, viewModel.Password);
            return await GetUserViewModel(user.Id);
        }
        #endregion

        #region Validations

        public bool CheckUserNameExist(string userName, Guid? id)
        {
            var users = _users.Select(a => new { Id = a.Id, a.UserName }).ToList();
            return id == null
                ? users.Any(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase))
                : users.Any(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase) && a.Id != id.Value);
        }

        public bool CheckEmailExist(string email, Guid? id)
        {
            email = email.FixGmailDots();
            return id == null
               ? _users.Any(a => a.Email == email.ToLower())
               : _users.Any(a => a.Email == email.ToLower() && a.Id != id.Value);
        }

        public bool CheckNameForShowExist(string nameForShow, Guid? id)
        {
            throw new NotImplementedException();
            //var namesForShow = _users.Select(a => new { Name = a.NameForShow, a.Id }).ToList();
            //nameForShow = nameForShow.GetFriendlyPersianName();
            //return id == null
            // ? namesForShow.Any(a => a.Name.GetFriendlyPersianName() == nameForShow)
            // : namesForShow.Any(a => a.Name.GetFriendlyPersianName() == nameForShow && a.Id != id.Value);
        }



        public bool CheckPhoneNumberExist(string phoneNumber, Guid? id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.Id != id.Value);
        }
        #endregion

        #region override GetRolesAsync
        public async override Task<IList<string>> GetRolesAsync(Guid userId)
        {
            var userPermissions = await _roleManager.FindUserPermissions(userId);
            ////todo: any permission form other sections
            return userPermissions;
        }

        public bool ShouldRefreshPerissions(Guid userId)
        {
            var user = _users.Find(userId);
            return user.IsChangedPermissions || user.IsBanned;
        }
        #endregion

        #region CustomValidatePasswordAsync
        public async Task<string> CustomValidatePasswordAsync(string pass)
        {
            var result = await PasswordValidator.ValidateAsync(pass);
            return result.Errors.GetUserManagerErros();
        }
        #endregion

        #region ChechIsBanneduser
        public bool CheckIsUserBanned(Guid id)
        {
            return _users.Any(a => a.Id == id && (a.IsBanned));
        }

        public bool CheckIsUserBanned(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && (a.IsBanned));
        }
        public bool CheckIsUserBannedByEmail(string email)
        {
            return _users.Any(a => a.Email == email.FixGmailDots() && (a.IsBanned));
        }


        #endregion

        #region GetEmailStore
        public IUserEmailStore<User, Guid> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, Guid>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }

        #endregion

        #region Private
        //private static string[] SplitString(string dependencies)
        //{
        //    if (dependencies == null) return new string[0];
        //    var split = from dependency in dependencies.Split(',')
        //                let lowerDependency = dependency.ToLower()
        //                where !string.IsNullOrEmpty(lowerDependency)
        //                select lowerDependency;
        //    return split.ToArray();
        //}
        private bool IsExistOneSystemAccount()
        {
            return _users.Any(a => a.IsSystemAccount);
        }
        #endregion

        #region IsEmailAvailableForConfirm
        public bool IsEmailAvailableForConfirm(string email)
        {
            email = email.FixGmailDots();
            return _users.Any(a => a.Email == email);
        }
        #endregion

        #region EditSecurityStamp
        public void EditSecurityStamp(Guid userId)
        {
            this.UpdateSecurityStamp(userId);
        }
        #endregion

        #region FindUserById
        public User FindUserById(Guid id)
        {
            return this.FindById(id);
        }
        #endregion

        #region CurrentUser
        public User GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _user ?? (_user = await FindByIdAsync(GetCurrentUserId()));
        }

        public Guid GetCurrentUserId()
        {
            return Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
        }

        public bool IsAdministrator()
        {
            return HttpContext.Current.User.IsInRole(StandardRoles.Administrators);
        }
        public bool IsOperator()
        {
            return HttpContext.Current.User.IsInRole(StandardRoles.Operators);
        }
        #endregion

        #region EditIsChangedPermissionsField

        public void EditIsChangedPermissionsField()
        {
            _unitOfWork.SaveChanges();
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Fill

        public async Task FillForEdit(EditUserViewModel viewModel)
        {
            viewModel.Roles = await _roleManager.GetAllAsSelectList();
            if (viewModel.RoleId.HasValue)
            {
                viewModel.Roles.ToList().ForEach(a => a.Selected = viewModel.RoleId.Value.ToString() == a.Value);
            }
        }


        #endregion

        #region DeleteAsync

        public Task DeleteAsync(Guid id)
        {
            return _users.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region IsSystemUser

        public Task<bool> IsSystemUser(Guid id)
        {
            return _users.AnyAsync(a => a.Id == id && a.IsSystemAccount);
        }
        #endregion

        #region Ban
        public async Task<UserViewModel> Ban(Guid id, bool flag)
        {
            var user = _users.Find(id);
            user.IsBanned = flag;
            await UpdateSecurityStampAsync(id);
            return await GetUserViewModel(user.Id);
        }
        #endregion

        #region GetUserViewModel
        public Task<UserViewModel> GetUserViewModel(Guid id)
        {
            return _users.AsNoTracking().ProjectTo<UserViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        public async Task<IEnumerable<SelectListItem>> GetAsSelectListItem()
        {
            var currentUserId = GetCurrentUserId();
            return await _users.Where(a => a.Id != currentUserId).ProjectTo<SelectListItem>(_mappingEngine).Cacheable(new EFCachePolicy { AbsoluteExpiration = DateTime.Now.AddDays(1) }).ToListAsync();
        }

        public long Count()
        {
            return _users.LongCount();
        }
    }
}
