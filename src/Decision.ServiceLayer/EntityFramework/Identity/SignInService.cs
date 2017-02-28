using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Decision.DomainClasses.Identity;
using Decision.ServiceLayer.Interfaces.Identity;
using Decision.ViewModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class SignInService : SignInManager<User, long>, ISignInService
    {
        #region Constructor

        public SignInService(UserService userService, IAuthenticationManager authenticationManager)
            : base(userService, authenticationManager)
        {
        }

        #endregion

        #region Public Methods

        public async Task<SignInResult> PasswordSignInAsync(LoginViewModel viewModel, bool shouldLockout)
        {
            if (UserManager == null)
            {
                throw new NullReferenceException($"{nameof(UserManager)}: is null in PasswordSignInAsync method");
            }

            var user = await UserManager.FindByNameAsync(viewModel.UserName.ToLowerInvariant()).ConfigureAwait(false);

            if (user == null)
            {
                return SignInResult.Failure;
            }

            if (await UserManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
            {
                return SignInResult.LockedOut;
            }
            if (await UserManager.CheckPasswordAsync(user, viewModel.Password).ConfigureAwait(false))
            {
                if (!user.EmailConfirmed) return SignInResult.RquiresConfirmation;
                if (!user.IsActive) return SignInResult.Passive;

                await UserManager.ResetAccessFailedCountAsync(user.Id).ConfigureAwait(false);
                return await SignInOrTwoFactor(user, viewModel.RememberMe).ConfigureAwait(false);
            }
            if (!shouldLockout) return SignInResult.Failure;

            await UserManager.AccessFailedAsync(user.Id).ConfigureAwait(false);
            if (await UserManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
            {
                return SignInResult.LockedOut;
            }

            return SignInResult.Failure;
        }

        #endregion

        #region Private Methods

        private async Task<SignInResult> SignInOrTwoFactor(User user, bool isPersistent)
        {
            var id = Convert.ToString(user.Id);
            if (await UserManager.GetTwoFactorEnabledAsync(user.Id)
                .ConfigureAwait(false) &&
                (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).ConfigureAwait(false)).Count > 0
                && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).ConfigureAwait(false))
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
                AuthenticationManager.SignIn(identity);
                return SignInResult.RequiresVerification;
            }

            await SignInAsync(user, isPersistent, false).ConfigureAwait(false);
            return SignInResult.Success;
        }

        #endregion
    }
}