//using System;
//using System.Data.Entity.Utilities;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security.Cookies;

//namespace Decision.Framework.IdentityToolkit.Validators
//{
//    public static class SecurityStampValidator
//    {
//        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TUserService, TUser>(
//            TimeSpan validateInterval, Func<TUserService, TUser, Task<ClaimsIdentity>> regenerateIdentity)
//            where TUser : class, IUser<long>
//            where TUserService : UserManager<TUser, long>
//        {
//            return OnValidateIdentity(validateInterval, regenerateIdentity, id => long.Parse(id.GetUserId()));
//        }
        
//        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TUserService, TUser>(
//               TimeSpan validateInterval, Func<TUserService, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback,
//               Func<ClaimsIdentity, long> getUserIdCallback)
//                where TUser : class, IUser<long>
//               where TUserService : UserManager<TUser, long>
//        {
//            if (getUserIdCallback == null)
//            {
//                throw new ArgumentNullException(nameof(getUserIdCallback));
//            }
//            return async context =>
//            {
//                var currentUtc = DateTimeOffset.UtcNow;
//                if (context.Options?.SystemClock != null)
//                {
//                    currentUtc = context.Options.SystemClock.UtcNow;
//                }
//                var issuedUtc = context.Properties.IssuedUtc;

//                // Only validate if enough time has elapsed
//                var validate = (issuedUtc == null);
//                if (issuedUtc != null)
//                {
//                    var timeElapsed = currentUtc.Subtract(issuedUtc.Value);
//                    validate = timeElapsed > validateInterval;
//                }

//                if (validate)
//                {
//                    var manager = context.OwinContext.GetUserManager<TUserService>();
//                    var userId = getUserIdCallback(context.Identity);
//                    if (manager != null)
//                    {
//                        var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
//                        var reject = true;
//                        // Refresh the identity if the stamp matches, otherwise reject
//                        if (user != null && manager.SupportsUserSecurityStamp)
//                        {
//                            var securityStamp =
//                                context.Identity.FindFirstValue(Microsoft.AspNet.Identity.Constants.DefaultSecurityStampClaimType);
//                            if (securityStamp == await manager.GetSecurityStampAsync(userId).WithCurrentCulture())
//                            {
//                                reject = false;
//                                // Regenerate fresh claims if possible and resign in
//                                if (regenerateIdentityCallback != null)
//                                {
//                                    var identity = await regenerateIdentityCallback.Invoke(manager, user).WithCurrentCulture();
//                                    if (identity != null)
//                                    {
//                                        // Fix for regression where this value is not updated
//                                        // Setting it to null so that it is refreshed by the cookie middleware
//                                        context.Properties.IssuedUtc = null;
//                                        context.Properties.ExpiresUtc = null;
//                                        context.OwinContext.Authentication.SignIn(context.Properties, identity);
//                                    }
//                                }
//                            }
//                        }
//                        if (reject)
//                        {
//                            context.RejectIdentity();
//                            context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
//                        }
//                    }
//                }
//            };
//        }
//    }
//}
