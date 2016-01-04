using System.Net;
using System.Web.Mvc;
using Decision.Common.Constants;
namespace Decision.Web.Controllers
{
    /// <summary>
    /// Provides methods that respond to HTTP requests with HTTP errors.
    /// </summary>
    [RoutePrefix("error")]
    public partial class ErrorController : Controller
    {
        #region Public Methods

        /// <summary>
        /// Returns a HTTP 400 Bad Request error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full bad request view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.BadRequest)]
        [Route("badrequest")]
        public virtual ActionResult BadRequest()
        {
            return this.GetErrorView(HttpStatusCode.BadRequest,MVC.Error.Views.ViewNames.BadRequest);
        }

        /// <summary>
        /// Returns a HTTP 403 Forbidden error view. Returns a partial view if the request is an AJAX call.
        /// Unlike a 401 Unauthorized response, authenticating will make no difference.
        /// </summary>
        /// <returns>The partial or full forbidden view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Forbidden)]
        [Route("forbidden")]
        public virtual ActionResult Forbidden()
        {
            return this.GetErrorView(HttpStatusCode.Forbidden, MVC.Error.Views.ViewNames.Forbidden);
        }

        /// <summary>
        /// Returns a HTTP 500 Internal Server Error error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full internal server error view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.InternalServerError)]
        [Route("internalservererror")]
        public virtual ActionResult InternalServerError()
        {
            return this.GetErrorView(HttpStatusCode.InternalServerError, MVC.Error.Views.ViewNames.InternalServerError);
        }

        /// <summary>
        /// Returns a HTTP 405 Method Not Allowed error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full method not allowed view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.MethodNotAllowed)]
        [Route("methodnotallowed")]
        public virtual ActionResult MethodNotAllowed()
        {
            return this.GetErrorView(HttpStatusCode.MethodNotAllowed, MVC.Error.Views.ViewNames.MethodNotAllowed);
        }

        /// <summary>
        /// Returns a HTTP 404 Not Found error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full not found view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.NotFound)]
        [Route("notfound")]
        public virtual ActionResult NotFound()
        {
            return this.GetErrorView(HttpStatusCode.NotFound, MVC.Error.Views.ViewNames.NotFound);
        }

        /// <summary>
        /// Returns a HTTP 401 Unauthorized error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full unauthorized view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Unauthorized)]
        [Route("unauthorized")]
        public virtual ActionResult Unauthorized()
        {
            return this.GetErrorView(HttpStatusCode.Unauthorized, MVC.Error.Views.ViewNames.Unauthorized);
        }
        [Route("lockout")]
        public virtual ActionResult LockOut()
        {
            return this.GetErrorView(HttpStatusCode.Forbidden, MVC.Error.Views.ViewNames.LockOut);
        }
        #endregion

        #region Private Methods
        private ActionResult GetErrorView(HttpStatusCode statusCode, string viewName)
        {
            this.Response.StatusCode = (int)statusCode;

            // Don't show IIS custom errors.
            this.Response.TrySkipIisCustomErrors = true;

            ActionResult result;
            if (this.Request.IsAjaxRequest())
            {
                // This allows us to show errors even in partial views.
                result = this.PartialView(viewName);
            }
            else
            {
                result = this.View(viewName);
            }

            return result;
        }

        #endregion
    }

}
