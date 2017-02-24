using System;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Extensions
{
    public static class UrlExtensions
    {
        public static string AbsoluteContent(this UrlHelper url, string contentPath)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;
            return $"{requestUrl.GetLeftPart(UriPartial.Authority)}{url.Content(contentPath)}";
        }
    }
}