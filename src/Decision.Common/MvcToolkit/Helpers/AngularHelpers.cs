using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Decision.Common.MvcToolkit.Helpers
{
    public static class AngularHelpers
    {

        #region Methods (2)
        public static IHtmlString AngularEditorForModel(this HtmlHelper helper, string modelPrefix)
        {
            // ReSharper disable once Mvc.TemplateNotResolved
            return helper.EditorForModel("Angular/Object", new { Prefix = modelPrefix });
        }

        public static IHtmlString AngularBindingForModel(this HtmlHelper helper)
        {
            var prefix = (string)helper.ViewBag.Prefix;
            if (prefix != null) prefix = $"{prefix}.";

            return MvcHtmlString.Create(value: $"{prefix}{helper.CamelCaseIdForModel()}");
        }

       
        #endregion

    }
}