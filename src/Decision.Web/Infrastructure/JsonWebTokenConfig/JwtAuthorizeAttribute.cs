using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Decision.Services.Interfaces.Security;
using Decision.Services.Interfaces.Users;

namespace Decision.Web.Infrastructure.JsonWebTokenConfig
{
    public sealed class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public Func<IUserService> UserService { set; get; }
        public Func<ITokenStoreService> TokenStoreService { set; get; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            var accessToken = actionContext.Request.Headers.Authorization.Parameter;
            if (string.IsNullOrWhiteSpace(accessToken) ||
                accessToken.Equals("undefined", StringComparison.OrdinalIgnoreCase))
            {
                // null token
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var claimsIdentity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                // this is not our issued token
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                // this is not our issued token
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var serialNumber = UserService?.Invoke().GetSerialNumber(long.Parse(userId));
            if (serialNumber != serialNumberClaim.Value)
            {
                // user has changed its password/roles/stat/IsActive
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            if (!TokenStoreService().IsValidToken(accessToken, long.Parse(userId)))
            {
                // this is not our issued token
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            base.OnAuthorization(actionContext);
        }


        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   ||
                   actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                       .Any();
        }
    }
}