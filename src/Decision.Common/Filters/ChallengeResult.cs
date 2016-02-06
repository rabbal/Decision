using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace Decision.Common.Filters
{
    public class ChallengeResult : HttpUnauthorizedResult
    {

        #region Fields
        // Used for XSRF protection when adding external logins
        public const string XsrfKey = "XsrfId";
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }
        #endregion


        #region Constructors

        public ChallengeResult(string provider, string redirectUri, string userId = null)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        #endregion


        #region Base Methods Override
        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
        #endregion

    }
}