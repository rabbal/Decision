using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Decision.Common.Extensions;

namespace Decision.Common.MvcToolkit.Extensions
{
    public static class ControllerExtentions
    {

        #region RenderViewToString

        public static string RenderPartialViewToString(this ControllerBase controller)
        {
            return RenderPartialViewToString(controller, null, null);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, string viewName)
        {
            return RenderPartialViewToString(controller, viewName, null);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, object model)
        {
            return RenderPartialViewToString(controller, null, model);
        }

        public static string RenderPartialViewToString(this ControllerBase controller, string viewName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string RenderViewToString(this ControllerBase controller)
        {
            return RenderViewToString(controller, null, null, null);
        }

        public static string RenderViewToString(this ControllerBase controller, object model)
        {
            return RenderViewToString(controller, null, null, model);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName)
        {
            return RenderViewToString(controller, viewName, null, null);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName)
        {
            return RenderViewToString(controller, viewName, masterName, null);
        }

        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName, object model)
        {
            if (viewName.IsEmpty())
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName);

                ThrowIfViewNotFound(viewResult, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

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

            throw new InvalidOperationException(
                $"The view '{viewName}' or its master was not found, searched locations: {locations}");
        }

        public static string RenderViewToString(this ControllerContext context,
        string viewPath,
        object model = null,
        bool partial = false)
        {
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

        #endregion

        public static bool IsEmbeddedIntoAnotherDomain(this ControllerBase controller)
        {

            var url = controller.ControllerContext.HttpContext.Request.Url;
            var urlReferrer = controller.ControllerContext.HttpContext.Request.UrlReferrer;
            return url != null && (urlReferrer != null &&
                                   !url.Host.Equals(urlReferrer.Host,
                                       StringComparison.InvariantCultureIgnoreCase));

        }
        public static HttpStatusCodeResult HttpInternalServerError(this System.Web.Mvc.Controller controller)
        {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        public static HttpStatusCodeResult HttpBadRequest(this System.Web.Mvc.Controller controller)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


    }
}