using System.Net;
using System.Web.Mvc;
using Decision.Web.Infrastructure.Constants;

namespace Decision.Web.Controllers
{
    /// <summary>
    ///     Provides methods that respond to HTTP requests with HTTP errors.
    /// </summary>
    [RoutePrefix("error")]
    public partial class ErrorController : Controller
    {
        #region Private Methods

        private ActionResult GetErrorView(HttpStatusCode statusCode, string viewName)
        {
            Response.StatusCode = (int) statusCode;

            // Don't show IIS custom errors.
            Response.TrySkipIisCustomErrors = true;

            ActionResult result;
            if (Request.IsAjaxRequest())
            {
                // This allows us to show errors even in partial views.
                result = PartialView(viewName);
            }
            else
            {
                result = View(viewName);
            }

            return result;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns a HTTP 400 Bad Request error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full bad request view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.BadRequest)]
        [Route("badrequest")]
        public virtual ActionResult BadRequest()
        {
            return GetErrorView(HttpStatusCode.BadRequest, MVC.Error.Views.BadRequest);
        }

        /// <summary>
        ///     Returns a HTTP 403 Forbidden error view. Returns a partial view if the request is an AJAX call.
        ///     Unlike a 401 Unauthorized response, authenticating will make no difference.
        /// </summary>
        /// <returns>The partial or full forbidden view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Forbidden)]
        [Route("forbidden")]
        public virtual ActionResult Forbidden()
        {
            return GetErrorView(HttpStatusCode.Forbidden, MVC.Error.Views.Forbidden);
        }

        /// <summary>
        ///     Returns a HTTP 500 Internal Server Error error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full internal server error view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.InternalServerError)]
        [Route("internalservererror")]
        public virtual ActionResult InternalServerError()
        {
            return GetErrorView(HttpStatusCode.InternalServerError, MVC.Error.Views.InternalServerError);
        }

        /// <summary>
        ///     Returns a HTTP 405 Method Not Allowed error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full method not allowed view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.MethodNotAllowed)]
        [Route("methodnotallowed")]
        public virtual ActionResult MethodNotAllowed()
        {
            return GetErrorView(HttpStatusCode.MethodNotAllowed, MVC.Error.Views.MethodNotAllowed);
        }

        /// <summary>
        ///     Returns a HTTP 404 Not Found error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full not found view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.NotFound)]
        [Route("notfound")]
        public virtual ActionResult NotFound()
        {
            return GetErrorView(HttpStatusCode.NotFound, MVC.Error.Views.NotFound);
        }

        /// <summary>
        ///     Returns a HTTP 401 Unauthorized error view. Returns a partial view if the request is an AJAX call.
        /// </summary>
        /// <returns>The partial or full unauthorized view.</returns>
        [OutputCache(CacheProfile = CacheProfileName.Unauthorized)]
        [Route("unauthorized")]
        public virtual ActionResult Unauthorized()
        {
            return GetErrorView(HttpStatusCode.Unauthorized, MVC.Error.Views.Unauthorized);
        }

        #endregion

    }
}