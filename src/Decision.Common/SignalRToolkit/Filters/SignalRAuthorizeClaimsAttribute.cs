using System;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;

namespace Decision.Common.SignalRToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SignalRAuthorizeClaimsAttribute : AuthorizeAttribute
    {
        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var principal = user as ClaimsPrincipal;

            if (principal == null) return false;

            var authenticated = principal.FindFirst(ClaimTypes.Authentication);

            return authenticated != null && authenticated.Value == "true";
        }
    }
}