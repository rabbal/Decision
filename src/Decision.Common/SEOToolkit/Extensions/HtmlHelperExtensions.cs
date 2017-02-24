using System.Web;
using System.Web.Mvc;
using Decision.Common.SEOToolkit.Referrer;
using NTierMvcFramework.Common.Utility;

namespace Decision.Common.SEOToolkit.Extensions
{
    /// <summary>
    ///     <see cref="HtmlHelper" /> extension methods.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        ///     Creates a string containing the referrer meta tags. <see cref="ReferrerMode" /> for more information.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="referrerMode">The type of referrer allowed to be sent.</param>
        /// <returns>The referrer meta tag.</returns>
        public static IHtmlString ReferrerMeta(this HtmlHelper htmlHelper, ReferrerMode referrerMode)
        {
            return referrerMode == ReferrerMode.NoneWhenDowngrade
                ? null
                : new MvcHtmlString("<meta name=\"referrer\" content=\"" + referrerMode.ToLowercaseString() + "\">");
        }

        public static IHtmlString RobotsMeta(this HtmlHelper htmlHelper, bool follow=true,bool index=true)
        {
            return new MvcHtmlString($"<meta name=\"robots\" content=\"{(index ? nameof(index) : "noindex")}, {(follow ? nameof(follow) : "nofollow")}\" />");

        }

        public static IHtmlString Title(this HtmlHelper htmlHelper,string title)
        {
            return new MvcHtmlString($"<title>{title.CorrectRtl()}</title>");
        }
    }
}