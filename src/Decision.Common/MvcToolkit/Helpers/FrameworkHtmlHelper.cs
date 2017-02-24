using System.Web;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Helpers
{
    public class FrameworkHtmlHelper
    {
        private readonly HtmlHelper _htmlHelper;
        public FrameworkHtmlHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        private static IHtmlString SubmitButton(string buttonText, string formId, string cssCalss)
        {
            const string loadingMessage = "درحال ارسال...";
            var html = $"<button type='button' onclick='framework.doSubmit(this, \"{formId}\")' " +
                       $"data-loading-text='{loadingMessage}' class='{cssCalss}'>{buttonText}</ button > ";

            return new MvcHtmlString(html);
        }

        public static IHtmlString SubmitInfoButton(string buttonText,string formId) => SubmitButton(buttonText, formId, "btn btn-info");
    }
}