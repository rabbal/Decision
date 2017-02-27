using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Decision.Framework.Extensions;
using Decision.Framework.GuardToolkit;
using Decision.Framework.KendoLinq;
using Decision.Framework.MvcToolkit.Json;
using Microsoft.AspNet.Identity;
using MvcThrottle;

namespace Decision.Framework.MvcToolkit.Controller
{
    [EnableThrottling]
    public abstract class ControllerBase : System.Web.Mvc.Controller
    {
        #region Protected Methods

        protected void StoreInTemp(string index, object value)
        {
            TempData[index] = value;
        }

        protected T RetrieveFromTemp<T>(string index)
        {
            return (T)TempData[index];
        }

        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (viewName.IsNullOrEmpty())
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected string RenderViewToString(object model)
        {
            return RenderViewToString(null, null, model);
        }

        protected string RenderViewToString(string viewName)
        {
            return RenderViewToString(viewName, null, null);
        }

        protected string RenderViewToString(string viewName, string masterName)
        {
            return RenderViewToString(viewName, masterName, null);
        }

        protected string RenderViewToString(string viewName, string masterName, object model)
        {
            if (viewName.IsNullOrWhiteSpace())
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ControllerContext, viewName, masterName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }



        protected string RenderViewToString(
            string viewPath,
            object model,
            bool partial = false)
        {
            var context = ControllerContext;
            var viewEngineResult = partial
                ? System.Web.Mvc.ViewEngines.Engines.FindPartialView(context, viewPath)
                : System.Web.Mvc.ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null) throw new FileNotFoundException("View cannot be found.");

            var view = viewEngineResult.View;

            context.Controller.ViewData.Model = model;

            string result = null;
            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }
            return result;
        }

        protected bool IsEmbeddedIntoAnotherDomain()
        {
            var url = ControllerContext.HttpContext.Request.Url;
            var urlReferrer = ControllerContext.HttpContext.Request.UrlReferrer;
            return url != null && (urlReferrer != null &&
                                   !url.Host.Equals(urlReferrer.Host,
                                       StringComparison.InvariantCultureIgnoreCase));
        }
        protected HttpStatusCodeResult HttpInternalServerError() => HttpInternalServerError(null);

        protected HttpStatusCodeResult HttpBadRequest() => HttpBadRequest(null);

        protected  HttpStatusCodeResult HttpGone() => HttpGone(null);

        protected virtual HttpStatusCodeResult HttpInternalServerError(string statusDescription )
        {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, statusDescription);
        }
        protected virtual HttpStatusCodeResult HttpBadRequest(string statusDescription)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, statusDescription);
        }

        protected virtual HttpStatusCodeResult HttpGone(string statusDescription)
        {
            return new HttpStatusCodeResult(HttpStatusCode.Gone, statusDescription);
        }
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected void AddErrors(string property, string error)
        {
            ModelState.AddModelError(property, error);
        }

        [Obsolete("Do not use standard Json Helper to return json data to the client. Use either JsonError or JsonSuccess instead.")]
        protected JsonResult Json<T>(T data)
        {
            throw Error.InvalidOperation("Do not use standard Json Helper to return json data to the client. Use either JsonError or JsonSuccess instead.");
        }

        protected StandardJsonResult JsonValidationError()
        {
            var result = new StandardJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }
            return result;
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult JsonSuccess<T>(T data) => new StandardJsonResult<T> { Data = data };
        protected JsonNetResult BetterJson<T>(T data) => new JsonNetResult { Data = data };
        protected JsonNetResult BetterJson<T>(T data, JsonRequestBehavior behavior) =>

            new JsonNetResult { JsonRequestBehavior = behavior, Data = data };
        protected JsonNetResult BetterJsonValidationError<T>(T data) where T : BaseListResponse
        {
            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                data.Errors.Add(validationError.ErrorMessage);
            }
            return new JsonNetResult { Data = data };
        }
        #endregion

        #region Private Methods
        private static void ThrowIfViewNotFound(ViewEngineResult viewResult, string viewName)
        {
            // if view not found, throw an exception with searched locations
            if (viewResult.View != null) return;
            var locations = new StringBuilder();
            locations.AppendLine();

            foreach (var location in viewResult.SearchedLocations)
            {
                locations.AppendLine(location);
            }

            throw Error.InvalidOperation($"The view '{viewName}' or its master was not found, searched locations: {locations}");
        }
        #endregion
    }
}

