using System.IO;
using System.Web.Mvc;

namespace Decision.Common.Json
{
    public static class JsonHelper
    {

        #region Convert View to String



        /// <summary>
        /// convert one view to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewPath"></param>
        /// <param name="model"></param>
        /// <param name="partial"></param>
        /// <returns></returns>
        public static string RenderViewToString(this ControllerContext context,
            string viewPath,
            object model = null,
            bool partial = false)
        {
            ViewEngineResult viewEngineResult = null;
            viewEngineResult = partial
                ? ViewEngines.Engines.FindPartialView(context, viewPath)
                : ViewEngines.Engines.FindView(context, viewPath, null);
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
    }
}