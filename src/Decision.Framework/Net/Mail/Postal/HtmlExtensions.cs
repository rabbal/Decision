using System;
using System.Web;
using System.Web.Mvc;

namespace Decision.Framework.Net.Mail.Postal
{
    public static class HtmlExtensions
    {
        public static IHtmlString EmbedImage(this HtmlHelper html, string imagePathOrUrl, string alt = "")
        {
            if (string.IsNullOrWhiteSpace(imagePathOrUrl)) throw new ArgumentException("Path or URL required", nameof(imagePathOrUrl));

            if (IsFileName(imagePathOrUrl))
            {
                imagePathOrUrl = html.ViewContext.HttpContext.Server.MapPath(imagePathOrUrl);
            }
            var imageEmbedder = (ImageEmbedder)html.ViewData[ImageEmbedder.ViewDataKey];
            var resource = imageEmbedder.ReferenceImage(imagePathOrUrl);
            return new HtmlString($"<img src=\"cid:{resource.ContentId}\" alt=\"{html.AttributeEncode(alt)}\"/>");
        }

        private static bool IsFileName(string pathOrUrl)
        {
            return !(pathOrUrl.StartsWith("http:", StringComparison.OrdinalIgnoreCase)
                     || pathOrUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase));
        }
    }
}
