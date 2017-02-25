using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Decision.Framework.IdentityToolkit
{
    public class CustomClaimsIdentityFactory : IClaimsIdentityFactory<User, long>
    {
        internal const string IdentityProviderClaimType =
            "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        internal const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";
        
        public CustomClaimsIdentityFactory()
        {
            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType;
            UserIdClaimType = ClaimTypes.NameIdentifier;
            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;
            SecurityStampClaimType = Microsoft.AspNet.Identity.Constants.DefaultSecurityStampClaimType;
        }
        
        public string RoleClaimType { get; set; }
        public string UserNameClaimType { get; set; }
        public string UserIdClaimType { get; set; }
        public string SecurityStampClaimType { get; set; }
        public virtual async Task<ClaimsIdentity> CreateAsync(UserManager<User, long> manager, User user, string authenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var id = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);
            id.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
            id.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
            if (manager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(SecurityStampClaimType,
                    await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
            }
            if (manager.SupportsUserRole)
            {
                var roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(RoleClaimType, roleName, ClaimValueTypes.String));
                }
            }
            if (manager.SupportsUserClaim)
            {
                id.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
            }
            return id;
        }

       
    }
}
