using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using Decision.Common.Controller.NotyHelper;

namespace Decision.Common.Controller
{
    public static class ControllerExtentions
    {

        #region RenderViewToString
        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller)
        {
            return RenderPartialViewToString(controller, null, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller, string viewName)
        {
            return RenderPartialViewToString(controller, viewName, null);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderPartialViewToString(this ControllerBase controller, object model)
        {
            return RenderPartialViewToString(controller, null, model);
        }

        /// <summary>
        /// Render partial view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
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

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller)
        {
            return RenderViewToString(controller, null, null, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, object model)
        {
            return RenderViewToString(controller, null, null, model);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="viewName">View name</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, string viewName)
        {
            return RenderViewToString(controller, viewName, null, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
        public static string RenderViewToString(this ControllerBase controller, string viewName, string masterName)
        {
            return RenderViewToString(controller, viewName, masterName, null);
        }

        /// <summary>
        /// Render view to string
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName">View name</param>
        /// <param name="masterName">Master name</param>
        /// <param name="model">Model</param>
        /// <returns>Result</returns>
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

            throw new InvalidOperationException(string.Format("The view '{0}' or its master was not found, searched locations: {1}", viewName, locations));
        }

        #endregion

        #region IsEmbeddedIntoAnotherDomain
        public static bool IsEmbeddedIntoAnotherDomain(this ControllerBase controller)
        {

            var url = controller.ControllerContext.HttpContext.Request.Url;
            var urlReferrer = controller.ControllerContext.HttpContext.Request.UrlReferrer;
            return url != null && (urlReferrer != null &&
                                   !url.Host.Equals(urlReferrer.Host,
                                       StringComparison.InvariantCultureIgnoreCase));

        }
        #endregion

        #region Noty
        private static void AddNotyAlert(this ControllerBase controller, NotyMessage message)
        {
            var noty = controller.TempData.ContainsKey(Noty.TempDataKey)
                 ? (Noty)controller.TempData[Noty.TempDataKey]
                 : new Noty();

            noty.AddNotyMessage(message);

            controller.TempData[Noty.TempDataKey] = noty;
        }
        public static void NotySuccessModal(this ControllerBase controller,string message, bool isSticky = false,MessageLocation location=MessageLocation.center)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.success,
                IsSticky = isSticky,
                Message = message,
                Location = location,
                CloseAnimation = AnimationTypes.bounceIn,
                OpenAnimation = AnimationTypes.bounceOut,
                IsModal = true,
                CloseWith = MessageCloseType.click
                
            };
            controller.AddNotyAlert(notyMessage);
        }
        public static void NotySuccess(this ControllerBase controller, string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.success,
                IsSticky = isSticky,
                Message = message,
                CloseWith = MessageCloseType.click,
                Location = MessageLocation.topLeft,
                CloseAnimation = AnimationTypes.bounceOut,
                OpenAnimation = AnimationTypes.bounce,
                IsModal = true
            };
            controller.AddNotyAlert(notyMessage);
        }
        public static void NotyInformation(this ControllerBase controller,string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.information,
                IsSticky = isSticky,
                Message = message
            };
           controller.AddNotyAlert(notyMessage);
        }

        public static void NotyWarning(this ControllerBase controller,string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.warning,
                IsSticky = isSticky,
                Message = message
            };
           controller.AddNotyAlert(notyMessage);
        }

        public static void NotyError(this ControllerBase controller,string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.error,
                IsSticky = isSticky,
                Message = message
            };
           controller.AddNotyAlert(notyMessage);
        }
        public static void NotyAlert(this ControllerBase controller,string message, bool isSticky = false)
        {
            var notyMessage = new NotyMessage
            {
                Type = AlertType.alert,
                IsSticky = isSticky,
                Message = message
            };
            controller.AddNotyAlert(notyMessage);
        }
        
        #endregion

        #region GetListOfErrors
        public static string GetListOfErrors(this ModelStateDictionary modelState)
        {
            var list = modelState.ToList();
            return
                list.Select(keyValuePair => keyValuePair.Value.Errors.Select(a => a.ErrorMessage))
                    .Aggregate(string.Empty,
                        (current1, errors) =>
                            errors.Aggregate(current1, (current, error) => current + string.Format("{0}\n", error)));
        }
        #endregion

        #region GetUserManagerErros

        public static string GetUserManagerErros(this IEnumerable<string> errors)
        {
            return errors.Aggregate(string.Empty, (current, error) => current + string.Format("{0} \n", error));
        }

        #endregion

     
    }
}