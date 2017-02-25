using System;
using System.Web.Mvc;

namespace Decision.Framework.MvcToolkit.Filters
{
    /// <summary>
    /// Requires that a HTTP request does not contain a trailing slash. If it does, return a 404 Not Found. This is 
    /// useful if you are dynamically generating something which acts like it's a file on the web server. 
    /// E.g. /Robots.txt/ should not have a trailing slash and should be /Robots.txt. Note, that we also don't care if 
    /// it is upper-case or lower-case in this instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NoTrailingSlashAttribute : FilterAttribute, IAuthorizationFilter
    {
        #region Constants
        private const char QueryCharacter = '?';
        private const char SlashCharacter = '/';

        #endregion

        #region Public Methods
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.HttpContext.Request.Url == null) return;

            var canonicalUrl = filterContext.HttpContext.Request.Url.ToString();
            var queryIndex = canonicalUrl.IndexOf(QueryCharacter);

            if (queryIndex == -1)
            {
                if (canonicalUrl[canonicalUrl.Length - 1] == SlashCharacter)
                {
                    HandleTrailingSlashRequest(filterContext);
                }
            }
            else
            {
                if (canonicalUrl[queryIndex - 1] == SlashCharacter)
                {
                    HandleTrailingSlashRequest(filterContext);
                }
            }
        }

        #endregion

        #region Private Methods
        private static void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
        #endregion
    }
}