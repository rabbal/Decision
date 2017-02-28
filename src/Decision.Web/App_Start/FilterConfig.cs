using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Decision.Framework.MvcToolkit.Filters;
using Decision.Web.Infrastructure.Filters;
using Decision.Web.Infrastructure.IocConfig;

namespace Decision.Web
{
    public static class FilterConfig
    {
        #region RegisterGlobalFilters

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            AddSearchEngineOptimizationFilters(filters);
            AddSecurityFilters(filters);
            AddContentSecurityPolicyFilters(filters);
            //ConfigureMvcThrottle(filters);
            filters.Add(IoC.Resolve<LayoutInfoAttribute>());
        }

        #endregion

        #region AddSearchEngineOptimizationFilters

        /// <summary>
        ///     Adds filters which help improve search engine optimization (SEO).
        /// </summary>
        /// <param name="filters">todo: describe filters parameter on AddSearchEngineOptimizationFilters</param>
        private static void AddSearchEngineOptimizationFilters(GlobalFilterCollection filters)
        {
            // filters.Add(new RedirectToCanonicalUrlAttribute(
            // RouteTable.Routes.AppendTrailingSlash,
            // RouteTable.Routes.LowercaseUrls));
        }

        #endregion

        #region AddSecurityFilters

        /// <summary>
        ///     Add filters to improve security.
        /// </summary>
        /// <param name="filters">todo: describe filters parameter on AddSecurityFilters</param>
        private static void AddSecurityFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RemoveServerHeaderAttribute());
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new ElmahRequestValidationErrorFilter());

            //// filters.Add(new RequireHttpsAttribute());
            //// Require HTTPS to be used across the whole site. System.Web.Mvc.RequireHttpsAttribute performs a
            //// 302 Temporary redirect from a HTTP URL to a HTTPS URL. This filter gives you the option to perform a
            //// 301 Permanent redirect or a 302 temporary redirect. You should perform a 301 permanent redirect if the
            //// page can only ever be accessed by HTTPS and a 302 temporary redirect if the page can be accessed over
            //// HTTP or HTTPS.

            //if (IoC.Resolve<IAppConfiguration>().RquiredHttps)
            //    filters.Add(new RedirectToHttpsAttribute(true));

            //// $Start-NWebSec$

            //// Several NWebsec Security Filters are added here. See
            //// http://rehansaeed.com/nwebsec-asp-net-mvc-security-through-http-headers/ and
            //// http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html and
            //// https://github.com/NWebsec/NWebsec/wiki for more information.
            //// Note: All of these filters can be applied to individual controllers and actions and indeed
            //// some of them only make sense when applied to a controller or action instead of globally here.

            //// Cache-Control: no-cache, no-store, must-revalidate
            //// Expires: -1
            //// Pragma: no-cache
            ////      Specifies whether appropriate headers to prevent browser caching should be set in the HTTP response.
            ////      Do not apply this attribute here globally, use it sparingly to disable caching. A good place to use
            ////      this would be on a page where you want to post back credit card information because caching credit
            ////      card information could be a security risk.
            //// filters.Add(new SetNoCacheHttpHeadersAttribute());

            //// X-Robots-Tag - Adds the X-Robots-Tag HTTP header. Disable robots from any action or controller this
            ////                attribute is applied to.
            //// filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true });

            //// X-Content-Type-Options - Adds the X-Content-Type-Options HTTP header. Stop IE9 and below from sniffing
            ////                          files and overriding the Content-Type header (MIME type).
            //filters.Add(new XContentTypeOptionsAttribute());

            //// X-Download-Options - Adds the X-Download-Options HTTP header. When users save the page, stops them from
            ////                      opening it and forces a save and manual open.
            //filters.Add(new XDownloadOptionsAttribute());

            //// X-Frame-Options - Adds the X-Frame-Options HTTP header. Stop clickjacking by stopping the page from
            ////                   opening in an iframe or only allowing it from the same origin.
            ////      Deny - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing
            ////             the browser to display the page when it is loaded in an iframe - but only if the iframe is
            ////             from the same origin as the page.
            ////      SameOrigin - Specifies that the X-Frame-Options header should be set in the HTTP response,
            ////                   instructing the browser to not display the page when it is loaded in an iframe.
            ////      Disabled - Specifies that the X-Frame-Options header should not be set in the HTTP response.
            //filters.Add(
            //    new XFrameOptionsAttribute
            //    {
            //        Policy = XFrameOptionsPolicy.Deny
            //    });
        }

        #endregion

        #region ConfigureMvcThrottle

        //private static void ConfigureMvcThrottle(GlobalFilterCollection filters)
        //{
        //    var throttleFilter = new ThrottlingFilter
        //    {
        //        Policy = new ThrottlePolicy(5, 10, 60 * 10, 600 * 10)
        //        {
        //            IpThrottling = true,
        //            IpRules = new Dictionary<string, RateLimits>
        //            {
        //                {"::1/10", new RateLimits {PerHour = 15}},
        //                {
        //                    "192.168.2.1",
        //                    new RateLimits {PerMinute = 30, PerHour = 30*60, PerDay = 30*60*24}
        //                }
        //            },
        //            IpWhitelist = new List<string>
        //            {
        //                "127.0.0.1",

        //                // Intranet
        //                "192.168.0.0 - 192.168.255.255",

        //                // Googlebot - update from http://iplists.com/nw/google.txt
        //                "64.68.1 - 64.68.255",
        //                "64.68.0.1 - 64.68.255.255",
        //                "64.233.0.1 - 64.233.255.255",
        //                "66.249.1 - 66.249.255",
        //                "66.249.0.1 - 66.249.255.255",
        //                "209.85.0.1 - 209.85.255.255",
        //                "209.185.1 - 209.185.255",
        //                "216.239.1 - 216.239.255",
        //                "216.239.0.1 - 216.239.255.255",

        //                // Bingbot
        //                "65.54.0.1 - 65.54.255.255",
        //                "68.54.1 - 68.55.255",
        //                "131.107.0.1 - 131.107.255.255",
        //                "157.55.0.1 - 157.55.255.255",
        //                "202.96.0.1 - 202.96.255.255",
        //                "204.95.0.1 - 204.95.255.255",
        //                "207.68.1 - 207.68.255",
        //                "207.68.0.1 - 207.68.255.255",
        //                "219.142.0.1 - 219.142.255.255",

        //                // Yahoo - update from http://user-agent-string.info/list-of-ua/bot-detail?bot=Yahoo!
        //                "67.195.0.1 - 67.195.255.255",
        //                "72.30.0.1 - 72.30.255.255",
        //                "74.6.0.1 - 74.6.255.255",
        //                "98.137.0.1 - 98.137.255.255",

        //                // Yandex - update from http://user-agent-string.info/list-of-ua/bot-detail?bot=YandexBot
        //                "100.43.0.1 - 100.43.255.255",
        //                "178.154.0.1 - 178.154.255.255",
        //                "199.21.0.1 - 199.21.255.255",
        //                "37.140.0.1 - 37.140.255.255",
        //                "5.255.0.1 - 5.255.255.255",
        //                "77.88.0.1 - 77.88.255.255",
        //                "87.250.0.1 - 87.250.255.255",
        //                "93.158.0.1 - 93.158.255.255",
        //                "95.108.0.1 - 95.108.255.255"
        //            },
        //            ClientThrottling = true,
        //            ClientWhitelist = new List<string> { "auth" },
        //            EndpointThrottling = true,
        //            EndpointType = EndpointThrottlingType.ControllerAndAction,
        //            EndpointRules = new Dictionary<string, RateLimits>
        //            {
        //                {"Home/", new RateLimits {PerHour = 90}},
        //                {"Account/", new RateLimits {PerHour = 90}},
        //                {"Home/About", new RateLimits {PerHour = 30}}
        //            }
        //        },
        //        Repository = new CacheRepository()
        //    };
        //    // filters.Add(throttleFilter);
        //}

        #endregion

        #region AddContentSecurityPolicyFilters

        /// <summary>
        ///     Adds the Content-Security-Policy (CSP) and/or Content-Security-Policy-Report-Only HTTP headers. This
        ///     creates a white-list from where various content in a web page can be loaded from. (See
        ///     <see cref="http://rehansaeed.com/content-security-policy-for-asp-net-mvc/" /> and
        ///     <see cref="http://developer.mozilla.org/en-US/docs/Web/Security/CSP/CSP_policy_directives" />
        ///     <see cref="http://github.com/NWebsec/NWebsec/wiki" /> and for more information).
        ///     Note: If you are using the 'Browser Link' feature of the Webs Essentials Visual Studio extension, it will
        ///     not work if you enable CSP (See
        ///     <see
        ///         cref="http://webessentials.uservoice.com/forums/140520-general/suggestions/6665824-browser-link-support-for-content-security-policy" />
        ///     ).
        ///     Note: All of these filters can be applied to individual controllers and actions e.g. If an action requires
        ///     access to content from YouTube.com, then you can add the following attribute to the action:
        ///     [CspFrameSrc(CustomSources = "*.youtube.com")].
        /// </summary>
        /// <param name="filters">todo: describe filters parameter on AddContentSecurityPolicyFilters</param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "Reviewed. Suppression is OK here.")]
        private static void AddContentSecurityPolicyFilters(GlobalFilterCollection filters)
        {
//            filters.Add(new CspAttribute());

//            // OR
//            // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable
//            //      logging of violations without blocking them. This is good for testing CSP without enabling it. To
//            //      make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions
//            //      e.g. CspDefaultSrcAttribute becomes CspDefaultSrcReportOnlyAttribute.
//            // filters.Add(new CspReportOnlyAttribute());

//            // Enables logging of CSP violations. See the NWebsecHttpHeaderSecurityModule_CspViolationReported method
//            // in Global.asax.cs to see where they are logged.
//            filters.Add(new CspReportUriAttribute { EnableBuiltinHandler = true });

//            filters.Add(
//                new CspDefaultSrcAttribute
//                {
//                    // Disallow everything from the same domain by default.
//                    None = true

//                    // Allow everything from the same domain by default.
//                    // Self = true
//                });

//            // base-uri - This directive restricts the document base URL
//            //            (See http://www.w3.org/TR/html5/infrastructure.html#document-base-url).
//            filters.Add(
//                new CspBaseUriAttribute
//                {
//                    // Allow base URL's from example.com.
//                    // CustomSources = "*.example.com",
//                    // Allow base URL's from the same domain.
//                    Self = false
//                });

//            // child-src - This directive restricts from where the protected resource can load web workers or embed
//            //             frames. This was introduced in CSP 2.0 to replace frame-src. frame-src should still be used
//            //             for older browsers.
//            filters.Add(
//                new CspChildSrcAttribute
//                {
//                    // Allow web workers or embed frames from example.com.
//                    // CustomSources = "*.example.com",
//                    // Allow web workers or embed frames from the same domain.
//                    Self = false
//                });

//            // connect-src - This directive restricts which URIs the protected resource can load using script interfaces
//            //               (Ajax Calls and Web Sockets).
//            filters.Add(
//                new CspConnectSrcAttribute
//                {
//#if DEBUG
//                    // Allow Browser Link to work in debug mode only.
//                    CustomSources = string.Join(" ", "localhost:*", "ws://localhost:*"),
//#else
//    // Allow AJAX and Web Sockets to example.com.
//    // CustomSources = "*.example.com",
//#endif
//                    // Allow all AJAX and Web Sockets calls from the same domain.
//                    Self = true
//                });

//            // font-src - This directive restricts from where the protected resource can load fonts.
//            filters.Add(
//                new CspFontSrcAttribute
//                {
//                    // Allow fonts from maxcdn.bootstrapcdn.com.
//                    CustomSources = string.Join(
//                        " ",
//                        ContentDeliveryNetwork.MaxCdn.Domain),

//                    // Allow all fonts from the same domain.
//                    Self = true
//                });

//            // form-action - This directive restricts which URLs can be used as the action of HTML form elements.
//            filters.Add(
//                new CspFormActionAttribute
//                {
//                    // Allow forms to post back to example.com.
//                    // CustomSources = "*.example.com",
//                    // Allow forms to post back to the same domain.
//                    Self = true
//                });

//            // frame-src - This directive restricts from where the protected resource can embed frames.
//            //             This is now deprecated in favour of child-src but should still be used for older browsers.
//            filters.Add(
//                new CspFrameSrcAttribute
//                {
//                    // Allow iFrames from example.com.
//                    // CustomSources = "*.example.com",
//                    // Allow iFrames from the same domain.
//                    Self = false
//                });

//            // frame-ancestors - This directive restricts from where the protected resource can embed frame, iframe,
//            //                   object, embed or applet's.
//            filters.Add(
//                new CspFrameAncestorsAttribute
//                {
//                    // Allow frame, iframe, object, embed or applet's from example.com.
//                    // CustomSources = "*.example.com",
//                    // Allow frame, iframe, object, embed or applet's from the same domain.
//                    Self = false
//                });

//            // img-src - This directive restricts from where the protected resource can load images.
//            filters.Add(
//                new CspImgSrcAttribute
//                {
//#if DEBUG
//                    // Allow Browser Link to work in debug mode only.
//                    CustomSources = "data:",
//#else
//                  CustomSources = "*.google-analytics.com"
//#endif
//                    // Allow images from the same domain.
//                    Self = true
//                });

//            // script-src - This directive restricts which scripts the protected resource can execute.
//            //              The directive also controls other resources, such as XSLT style sheets, which can cause the
//            //              user agent to execute script.
//            filters.Add(
//                new CspScriptSrcAttribute
//                {
//                    // Allow scripts from the CDN's.
//                    CustomSources = string.Join(
//                        " ",
//#if DEBUG
//                        // Allow Browser Link to work in debug mode only.
//                        "localhost:*",
//#endif
//                        "*.google-analytics.com",
//                        ContentDeliveryNetwork.Microsoft.Domain),

//                    // Allow scripts from the same domain.
//                    Self = true,

//                    // Allow the use of the eval() method to create code from strings. This is unsafe and can open your
//                    // site up to XSS vulnerabilities.
//                    // UnsafeEval = true,
//                    // Allow in-line JavaScript, this is unsafe and can open your site up to XSS vulnerabilities.
//                    UnsafeInline = true
//                });

//            // media-src - This directive restricts from where the protected resource can load video and audio.
//            filters.Add(
//                new CspMediaSrcAttribute
//                {
//                    // Allow audio and video from example.com.
//                    // CustomSources = "example.com",
//                    // Allow audio and video from the same domain.
//                    Self = false
//                });

//            // object-src - This directive restricts from where the protected resource can load plug-ins.
//            filters.Add(
//                new CspObjectSrcAttribute
//                {
//                    // Allow plug-ins from example.com.
//                    // CustomSources = "example.com",
//                    // Allow plug-ins from the same domain.
//                    Self = false
//                });

//            // plugin-types - This directive restricts the set of plug-ins that can be invoked by the protected resource.
//            //                You can also use the @Html.CspMediaType("application/pdf") HTML helper instead of this
//            //                attribute. The HTML helper will add the media type to the CSP header.
//            // filters.Add(
//            //     new CspPluginTypesAttribute()
//            //     {
//            //         // Allow Adobe Flash and Microsoft Silverlight plug-ins
//            //         MediaTypes = "application/x-shockwave-flash application/xaml+xml"
//            //     });
//            // style-src - This directive restricts which styles the user applies to the protected resource.
//            filters.Add(
//                new CspStyleSrcAttribute
//                {
//                    // Allow CSS from maxcdn.bootstrapcdn.com
//                    CustomSources = string.Join(
//                        " ",
//                        ContentDeliveryNetwork.MaxCdn.Domain),

//                    // Allow CSS from the same domain.
//                    Self = true,

//                    // Allow in-line CSS, this is unsafe and can open your site up to XSS vulnerabilities.
//                    // Note: This is currently enable because Modernizr does not support CSP and includes in-line styles
//                    // in its JavaScript files. This is a security hold. If you don't want to use Modernizr, be sure to
//                    // disable unsafe in-line styles. For more information See:
//                    // http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
//                    // https://github.com/Modernizr/Modernizr/pull/1263
//                    UnsafeInline = true
//                });
        }

        #endregion
    }
}
