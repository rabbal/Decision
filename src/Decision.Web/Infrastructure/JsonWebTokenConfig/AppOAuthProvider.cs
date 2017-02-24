using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Decision.DomainClasses.Users;
using Decision.Services.Interfaces.Security;
using Decision.Services.Interfaces.Users;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAppJwtConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly Func<ITokenStoreService> _tokenStoreService;
        private readonly Func<IUserService> _userService;

        public AppOAuthProvider(
            Func<IUserService> userService,
            Func<ITokenStoreService> tokenStoreService,
            ISecurityService securityService,
            IAppJwtConfiguration configuration)
        {
            _userService = userService;
            _tokenStoreService = tokenStoreService;
            _securityService = securityService;
            _configuration = configuration;
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId != null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            // Change authentication ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            var userId = long.Parse(context.Ticket.Identity.FindFirstValue(ClaimTypes.UserData));
            _userService().UpdateLastLoggedIn(userId);

            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await _userService().FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "نام کاربری و کلمه عبور صحیح نمیباشد.");
                context.Rejected();
            }

            var identity = SetClaimsIdentity(context, user);
            context.Validated(identity);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            context.Validated();
            return Task.FromResult(0);
        }

        private ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, User user)
        {
            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.SerialNumber, user.SecurityStamp));
            identity.AddClaim(new Claim(ClaimTypes.UserData, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.DisplayName));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

            var roles = _userService().GetRoleNames(user.Id);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return identity;
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            _tokenStoreService().UpdateUserToken(
                long.Parse(context.Identity.FindFirstValue(ClaimTypes.UserData)),
                _securityService.GetSha256Hash(context.AccessToken)
                );

            return base.TokenEndpointResponse(context);
        }
    }
}