using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Decision.Common.Filters
{
    public class ForceWww : ActionFilterAttribute
    {

        #region Fields
        private readonly string _baseHost;
        private ActionExecutingContext _filterContext;
        private HttpRequestBase _request;
        #endregion

        #region Constructor
        public ForceWww(string siteRootUrl)
        {
            _baseHost = new Uri(siteRootUrl).Host.ToLowerInvariant();
        }
        #endregion

        #region CanIgnoreRequest
        private bool CanIgnoreRequest
        {
            get
            {
                var url = _request.Url;
                return url != null &&
                    (_filterContext.IsChildAction ||
                     _filterContext.HttpContext.Request.IsAjaxRequest() ||
                     url.AbsoluteUri.Contains("?"));
            }
        }
        #endregion

        #region IsDomainSetCorrectly
        private bool IsDomainSetCorrectly
        {
            get
            {
                var url = _request.Url;
                return (url != null) && (url.Host == _baseHost);
            }
        }
        #endregion

        #region IsLocalRequest
        private bool IsLocalRequest => _request.IsLocal;

        #endregion

        #region IsRootRequest
        private bool IsRootRequest
        {
            get
            {
                var url = _request.Url;
                return url != null && url.AbsolutePath == "/";
            }
        }
        #endregion

        #region OnActionExecuting Override
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _filterContext = filterContext;
            _request = _filterContext.RequestContext.HttpContext.Request;
            ModifyUrlAndRedirectPermanent();

            base.OnActionExecuting(filterContext);
        }

        #endregion

        #region AvoidTrailingSlashes
        private string AvoidTrailingSlashes(string url)
        {
            if (!IsRootRequest && url.EndsWith("/"))
            {
                url = url.TrimEnd(new[] { '/' });
            }
            return url;
        }
        #endregion

        #region ModifyUrlAndRedirectPermanent
        private void ModifyUrlAndRedirectPermanent()
        {
            if (IsLocalRequest || IsDomainSetCorrectly || CanIgnoreRequest)
                return;

            var url = _request.Url;
            if (url == null)
                return;

            var newUri = new UriBuilder(url) { Host = _baseHost };
            var absoluteUrl = HttpUtility.UrlDecode(newUri.Uri.AbsoluteUri.ToString(CultureInfo.InvariantCulture));
            if (string.IsNullOrWhiteSpace(absoluteUrl))
                return;

            var redirectUrl = absoluteUrl.ToLowerInvariant();
            redirectUrl = AvoidTrailingSlashes(redirectUrl);
            _filterContext.Controller.ViewBag.CanonicalUrl = redirectUrl;

            _filterContext.Result = new RedirectResult(redirectUrl, permanent: true);
        }
        #endregion

    }
}