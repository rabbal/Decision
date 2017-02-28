using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.DomainClasses.Identity;
using Decision.ViewModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace Decision.ServiceLayer.Interfaces.Identity
{
    public interface IUserService : IDisposable
    {
        void SeedDatabase();
        bool IsDisposableEmail(string email);
        bool CheckUserNameExist(string userName, long? id);
        bool CheckEmailExist(string email, long? id);
        bool CheckPhoneNumberExist(string phoneNumber, long? id);
        Task<bool> IsDisposableEmailAsync(string email);
        Task<bool> CheckUserNameExistAsync(string userName, long? id);
        Task<bool> CheckEmailExistAsync(string email, long? id);
        Task<bool> CheckPhoneNumberExistAsync(string phoneNumber, long? id);
        SelectListItem[] GetAvailableUsers();
        Func<CookieValidateIdentityContext, Task> OnValidateIdentity();
        Task<ClaimsIdentity> GenerateUserIdentityAsync(User user);
        IUserEmailStore<User, long> GetEmailStore();
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task<User> FindByIdAsync(long userId);
        Task<User> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(User user, string password);
       // Task<IdentityResult> RegisterAsync(RegisterViewModel viewModel);
        Task<User> FindAsync(string userName, string password);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> HasPasswordAsync(long userId);
        Task<IdentityResult> AddPasswordAsync(long userId, string password);
        Task<IdentityResult> ChangePasswordAsync(long userId, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(long userId);
        Task<string> GetSecurityStampAsync(long userId);
        Task<IdentityResult> UpdateSecurityStampAsync(long userId);
        Task<string> GeneratePasswordResetTokenAsync(long userId);
        Task<IdentityResult> ResetPasswordAsync(long userId, string token, string newPassword);
        string GetSerialNumber(long userId);
        Task<User> FindAsync(UserLoginInfo login);
        Task<IdentityResult> RemoveLoginAsync(long userId, UserLoginInfo login);
        Task<IdentityResult> AddLoginAsync(long userId, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(long userId);
        Task<IdentityResult> AddClaimAsync(long userId, Claim claim);
        Task<IdentityResult> RemoveClaimAsync(long userId, Claim claim);
        Task<IList<Claim>> GetClaimsAsync(long userId);
        Task<IdentityResult> AddToRoleAsync(long userId, string role);
        Task<IdentityResult> AddToRolesAsync(long userId, params string[] roles);
        Task<IdentityResult> RemoveFromRolesAsync(long userId, params string[] roles);
        Task<IdentityResult> RemoveFromRoleAsync(long userId, string role);
        Task<IList<string>> GetRolesAsync(long userId);
        Task<bool> IsInRoleAsync(long userId, string role);
        Task<string> GetEmailAsync(long userId);
        Task<IdentityResult> SetEmailAsync(long userId, string email);
        Task<User> FindByEmailAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(long userId);
        Task<IdentityResult> ConfirmEmailAsync(long userId, string token);
        Task<bool> IsEmailConfirmedAsync(long userId);
        Task<string> GetPhoneNumberAsync(long userId);
        Task<IdentityResult> SetPhoneNumberAsync(long userId, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(long userId, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(long userId);
        Task<string> GenerateChangePhoneNumberTokenAsync(long userId, string phoneNumber);
        IList<string> GetRoleNames(long id);
        Task<bool> VerifyChangePhoneNumberTokenAsync(long userId, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(long userId, string purpose, string token);
        Task<string> GenerateUserTokenAsync(string purpose, long userId);
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, long> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(long userId);
        Task<bool> VerifyTwoFactorTokenAsync(long userId, string twoFactorProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(long userId, string twoFactorProvider);
        Task<IdentityResult> NotifyTwoFactorTokenAsync(long userId, string twoFactorProvider, string token);
        Task<bool> GetTwoFactorEnabledAsync(long userId);
        Task<IdentityResult> SetTwoFactorEnabledAsync(long userId, bool enabled);
        Task SendEmailAsync(long userId, string subject, string body);
        Task SendSmsAsync(long userId, string message);
        Task<bool> IsLockedOutAsync(long userId);
        Task<IdentityResult> SetLockoutEnabledAsync(long userId, bool enabled);
        Task<bool> GetLockoutEnabledAsync(long userId);
        Task<DateTimeOffset> GetLockoutEndDateAsync(long userId);
        Task<IdentityResult> SetLockoutEndDateAsync(long userId, DateTimeOffset lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(long userId);
        Task<IdentityResult> ResetAccessFailedCountAsync(long userId);
        Task<int> GetAccessFailedCountAsync(long userId);
        IPasswordHasher PasswordHasher { get; set; }
        IIdentityValidator<User> UserValidator { get; set; }
        IIdentityValidator<string> PasswordValidator { get; set; }
        IClaimsIdentityFactory<User, long> ClaimsIdentityFactory { get; set; }
        IIdentityMessageService EmailService { get; set; }
        IIdentityMessageService SmsService { get; set; }
        IUserTokenProvider<User, long> UserTokenProvider { get; set; }
        bool UserLockoutEnabledByDefault { get; set; }
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<User> Users { get; }
        IDictionary<string, IUserTokenProvider<User, long>> TwoFactorProviders { get; }
        Task<bool> CheckIsBannedAsync(string userName);
        bool CheckIsBanned(string userName);
        void Create(AddUserViewModel viewModel);
        Task CreateAsync(AddUserViewModel viewModel);
    }
}