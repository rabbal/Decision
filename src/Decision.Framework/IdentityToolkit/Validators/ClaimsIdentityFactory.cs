//using System;
//using System.Data.Entity.Utilities;
//using System.Globalization;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;

//namespace Decision.Framework.IdentityToolkit.Validators
//{
//    public sealed class ClaimsIdentityFactory<TUser> : IClaimsIdentityFactory<TUser, long>
//        where TUser : class, IUser<long>
//    {
//        const string IdentityProviderClaimType =
//           "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

//        const string DefaultIdentityProviderClaimValue = "ASP.NET Identity";

//        public ClaimsIdentityFactory()
//        {
//            RoleClaimType = ClaimsIdentity.DefaultRoleClaimType;
//            UserIdClaimType = ClaimTypes.NameIdentifier;
//            UserNameClaimType = ClaimsIdentity.DefaultNameClaimType;
//            SecurityStampClaimType = Microsoft.AspNet.Identity.Constants.DefaultSecurityStampClaimType;
//        }

//        /// <summary>
//        ///     Claim type used for role claims
//        /// </summary>
//        public string RoleClaimType { get; set; }

//        /// <summary>
//        ///     Claim type used for the user name
//        /// </summary>
//        public string UserNameClaimType { get; set; }

//        /// <summary>
//        ///     Claim type used for the user id
//        /// </summary>
//        public string UserIdClaimType { get; set; }

//        /// <summary>
//        ///     Claim type used for the user security stamp
//        /// </summary>
//        public string SecurityStampClaimType { get; set; }

//        /// <summary>
//        ///     Create a ClaimsIdentity from a user
//        /// </summary>
//        /// <param name="manager"></param>
//        /// <param name="user"></param>
//        /// <param name="authenticationType"></param>
//        /// <returns></returns>
//        public async Task<ClaimsIdentity> CreateAsync(UserManager<TUser, long> manager, TUser user,
//            string authenticationType)
//        {
//            if (manager == null)
//            {
//                throw new ArgumentNullException(nameof(manager));
//            }
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user));
//            }

//            var identity = new ClaimsIdentity(authenticationType, UserNameClaimType, RoleClaimType);

//            identity.AddClaim(new Claim(UserIdClaimType, user.Id.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.String));
//            identity.AddClaim(new Claim(UserNameClaimType, user.UserName, ClaimValueTypes.String));
//            identity.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue,
//                ClaimValueTypes.String));

//            //var displayName = string.IsNullOrEmpty(user.DisplayName) ? user.UserName : user.DisplayName;
//            //identity.AddClaim(new Claim(ClaimTypes.Surname, displayName));
//            ////identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

//            if (manager.SupportsUserSecurityStamp)
//            {
//                identity.AddClaim(new Claim(SecurityStampClaimType,
//                    await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()));
//            }
//            if (manager.SupportsUserRole)
//            {
//                var roles = await manager.GetRolesAsync(user.Id).WithCurrentCulture();
//                foreach (var roleName in roles)
//                {
//                    identity.AddClaim(new Claim(RoleClaimType, roleName, ClaimValueTypes.String));
//                }
//            }

//            if (manager.SupportsUserClaim)
//            {
//                identity.AddClaims(await manager.GetClaimsAsync(user.Id).WithCurrentCulture());
//            }

//            return identity;
//        }
//    }
//}