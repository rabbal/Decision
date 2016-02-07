using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
namespace Decision.Web.HtmlHelpers
{
    public static class T4SecureExtensions
    {
      

        public static MvcHtmlString SideBarSecureActionLink(this HtmlHelper htmlHelper, string linkText, string url, string cssClass, string spanCssClass, params string[] permission)
        {
            var hasPermission = permission.Any(HttpContext.Current.User.IsInRole);
            if (!hasPermission) return MvcHtmlString.Empty;

            var a = new TagBuilder("a");
            a.Attributes.Add("href", url);
            a.AddCssClass(cssClass);
            var span = new TagBuilder("span");
            span.AddCssClass(spanCssClass);
            a.InnerHtml = span.ToString(TagRenderMode.Normal) + linkText;

            return MvcHtmlString.Create(a.ToString());

        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result)
        {
            return htmlHelper.SecureActionLink(linkText, result, null, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes, string protocol)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, null, protocol ?? result.GetT4MVCResult().Protocol, hostName, fragment, result.GetRouteValueDictionary(), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.SecureActionLink(linkText, result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, null, protocol ?? result.GetT4MVCResult().Protocol, hostName, fragment, result.GetRouteValueDictionary(), htmlAttributes);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, object htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, null, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, object htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, object htmlAttributes, string protocol)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, object htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, object htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), protocol, hostName, fragment);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, null, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.RouteLink(linkText, routeName, result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, ActionResult result, IDictionary<string, object> htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, routeName, protocol ?? result.GetT4MVCResult().Protocol, hostName, fragment, result.GetRouteValueDictionary(), htmlAttributes);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, ActionResult result)
        {
            return htmlHelper.SecureBeginForm(result, FormMethod.Post);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod)
        {
            return htmlHelper.SecureBeginForm(result, formMethod, null);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod, object htmlAttributes)
        {
            return SecureBeginForm(htmlHelper, result, formMethod, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, ActionResult result, FormMethod formMethod, IDictionary<string, object> htmlAttributes)
        {
            var callInfo = result.GetT4MVCResult();
            return htmlHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, formMethod, htmlAttributes);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, ActionResult result)
        {
            return htmlHelper.SecureBeginRouteForm(null, result, FormMethod.Post, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, ActionResult result)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, result, FormMethod.Post, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, ActionResult result, FormMethod method)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, result, method, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, ActionResult result, FormMethod method, object htmlAttributes)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, result, method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, ActionResult result, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.BeginRouteForm(routeName, result.GetRouteValueDictionary(), method, htmlAttributes);
        }

        public static void RenderSecureAction(this HtmlHelper htmlHelper, ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            htmlHelper.RenderAction(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary);
        }

        public static MvcHtmlString SecureAction(this HtmlHelper htmlHelper, ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return htmlHelper.Action(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, null, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, object htmlAttributes)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, object htmlAttributes, string protocol)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, object htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, object htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, null, protocol ?? taskResult.Result.GetT4MVCResult().Protocol, hostName, fragment, taskResult.Result.GetRouteValueDictionary(), HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.SecureActionLink(linkText, taskResult.Result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString SecureActionLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, null, protocol ?? taskResult.Result.GetT4MVCResult().Protocol, hostName, fragment, taskResult.Result.GetRouteValueDictionary(), htmlAttributes);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, object htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, null, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, object htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, object htmlAttributes, string protocol)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, object htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, object htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), protocol, hostName, fragment);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, null, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, null, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, protocol, null, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol, string hostName)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, protocol, hostName, null);
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName, Task<ActionResult> taskResult, IDictionary<string, object> htmlAttributes, string protocol, string hostName, string fragment)
        {
            return htmlHelper.RouteLink(linkText, routeName, taskResult.Result, htmlAttributes, protocol, hostName, fragment);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, Task<ActionResult> taskResult)
        {
            return htmlHelper.SecureBeginForm(taskResult.Result, FormMethod.Post);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, Task<ActionResult> taskResult, FormMethod formMethod)
        {
            return htmlHelper.SecureBeginForm(taskResult.Result, formMethod, null);
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, Task<ActionResult> taskResult, FormMethod formMethod, object htmlAttributes)
        {
            return SecureBeginForm(htmlHelper, taskResult.Result, formMethod, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginForm(this HtmlHelper htmlHelper, Task<ActionResult> taskResult, FormMethod formMethod, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SecureBeginForm(taskResult.Result, formMethod, htmlAttributes);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, Task<ActionResult> taskResult)
        {
            return htmlHelper.SecureBeginRouteForm(null, taskResult.Result, FormMethod.Post, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, Task<ActionResult> taskResult)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, taskResult.Result, FormMethod.Post, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, Task<ActionResult> taskResult, FormMethod method)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, taskResult.Result, method, null);
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, Task<ActionResult> taskResult, FormMethod method, object htmlAttributes)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, taskResult.Result, method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginRouteForm(this HtmlHelper htmlHelper, string routeName, Task<ActionResult> taskResult, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.SecureBeginRouteForm(routeName, taskResult.Result, method, htmlAttributes);
        }

        public static void RenderSecureAction(this HtmlHelper htmlHelper, Task<ActionResult> taskResult)
        {
            htmlHelper.RenderSecureAction(taskResult.Result);
        }

        public static MvcHtmlString SecureAction(this HtmlHelper htmlHelper, Task<ActionResult> taskResult)
        {
            return htmlHelper.SecureAction(taskResult.Result);
        }

        /// <summary>
        /// If specific route can be found, return that route with the parameter tokens in route string.
        /// </summary>
        public static string JavaScriptReplaceableUrl(this UrlHelper urlHelper, ActionResult result)
        {
            var rvd = result.GetRouteValueDictionary();
            string area = string.Empty;
            object token;

            if (rvd.TryGetValue("area", out token))
                area = token.ToString();

            if (!rvd.TryGetValue("controller", out token))
                throw new Exception("T4MVC JavascriptReplacableUrl could not locate controller in source dictionary");
            string controller = token.ToString();

            if (!rvd.TryGetValue("SecureAction", out token))
                throw new Exception("T4MVC JavascriptReplacableUrl could not locate SecureAction in source dictionary");
            string SecureAction = token.ToString();

            // This matches the ActionResult to a specific route (so we can get the exact URL)
            string specificSecureActionUrl = RouteTable.Routes.OfType<Route>()
                .Where(r => r.DataTokens.CompareValue("area", area)
                    && r.Defaults.CompareValue("controller", controller)
                    && r.Defaults.CompareValue("SecureAction", SecureAction))
                .Select(r => r.Url)
                .FirstOrDefault();

            if (String.IsNullOrEmpty(specificSecureActionUrl))
            {
                return urlHelper.RouteUrl(null, result.GetRouteValueDictionary());
            }

            return urlHelper.Content("~/" + specificSecureActionUrl);
        }

        // This compares a specified value with that in a RouteValueDictionary for a given key
        // while ignoring null dictionaries if the match value is also null or whitespace
        private static bool CompareValue(this RouteValueDictionary dictionary, string key, string value)
        {
            // Normalize the value to null if empty or whitespace
            if (string.IsNullOrWhiteSpace(value))
            {
                value = null;
            }

            // Do we actually have the RouteValueDictionary
            if (dictionary == null)
            {
                // No dictionary, match if the value is also null or whitespace
                return value == null;
            }
            else
            {
                // Match if the value is null or whitespace and the key is not in the dictionary
                // or if the key is in the dictionary and matches the value
                return string.Compare(value, dictionary
                    .Where(kvp => string.Compare(kvp.Key, key, StringComparison.InvariantCultureIgnoreCase) == 0)
                    .Select(kvp => kvp.Value.ToString())
                    .FirstOrDefault(), StringComparison.InvariantCultureIgnoreCase) == 0;
            }
        }

        public static string JavaScriptReplaceableUrl(this UrlHelper urlHelper, Task<ActionResult> taskResult)
        {
            return urlHelper.JavaScriptReplaceableUrl(taskResult.Result);
        }

        public static string SecureAction(this UrlHelper urlHelper, ActionResult result)
        {
            return urlHelper.SecureAction(result, null, null);
        }

        public static string SecureAction(this UrlHelper urlHelper, ActionResult result, string protocol = null, string hostName = null)
        {
            return urlHelper.RouteUrl(null, result.GetRouteValueDictionary(), protocol ?? result.GetT4MVCResult().Protocol, hostName);
        }

        public static string SecureAction(this UrlHelper urlHelper, Task<ActionResult> taskResult)
        {
            return urlHelper.SecureAction(taskResult.Result, null, null);
        }

        public static string SecureAction(this UrlHelper urlHelper, Task<ActionResult> taskResult, string protocol = null, string hostName = null)
        {
            return urlHelper.SecureAction(taskResult.Result, protocol, hostName);
        }

        private static string SecureActionAbsolute(this UrlHelper urlHelper, ActionResult result)
        {
            return
                $"{urlHelper.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)}{urlHelper.RouteUrl(T4Extensions.GetRouteValueDictionary(result))}";
        }

        public static string SecureActionAbsolute(this UrlHelper urlHelper, Task<ActionResult> taskResult)
        {
            return urlHelper.SecureActionAbsolute(taskResult.Result);
        }

        public static string RouteUrl(this UrlHelper urlHelper, ActionResult result)
        {
            return urlHelper.RouteUrl(null, result, null, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, ActionResult result)
        {
            return urlHelper.RouteUrl(routeName, result, null, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, ActionResult result, string protocol)
        {
            return urlHelper.RouteUrl(routeName, result, protocol, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, ActionResult result, string protocol, string hostName)
        {
            return urlHelper.RouteUrl(routeName, result.GetRouteValueDictionary(), protocol ?? result.GetT4MVCResult().Protocol, hostName);
        }

        public static string RouteUrl(this UrlHelper urlHelper, Task<ActionResult> taskResult)
        {
            return urlHelper.RouteUrl(null, taskResult.Result, null, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, Task<ActionResult> taskResult)
        {
            return urlHelper.RouteUrl(routeName, taskResult.Result, null, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, Task<ActionResult> taskResult, string protocol)
        {
            return urlHelper.RouteUrl(routeName, taskResult.Result, protocol, null);
        }

        public static string RouteUrl(this UrlHelper urlHelper, string routeName, Task<ActionResult> taskResult, string protocol, string hostName)
        {
            return urlHelper.RouteUrl(routeName, taskResult.Result, protocol, hostName);
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions);
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, ActionResult result, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, result.GetRouteValueDictionary(), ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, Task<ActionResult> taskResult, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.SecureActionLink(linkText, taskResult.Result, ajaxOptions);
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.SecureActionLink(linkText, taskResult.Result, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString SecureActionLink(this AjaxHelper ajaxHelper, string linkText, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.SecureActionLink(linkText, taskResult.Result, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, ActionResult result, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.RouteLink(linkText, routeName, result, ajaxOptions, null);
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, ActionResult result, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, routeName, result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, ActionResult result, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, routeName, result.GetRouteValueDictionary(), ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.RouteLink(linkText, routeName, taskResult.Result, ajaxOptions, null);
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, routeName, taskResult.Result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString RouteLink(this AjaxHelper ajaxHelper, string linkText, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.RouteLink(linkText, routeName, taskResult.Result, ajaxOptions, htmlAttributes);
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, ActionResult result, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.SecureBeginForm(result, ajaxOptions, null);
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, ActionResult result, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return SecureBeginForm(ajaxHelper, result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, ActionResult result, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            var callInfo = result.GetT4MVCResult();
            return ajaxHelper.BeginForm(callInfo.Action, callInfo.Controller, callInfo.RouteValueDictionary, ajaxOptions, htmlAttributes);
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, Task<ActionResult> taskResult, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.SecureBeginForm(taskResult.Result, ajaxOptions, null);
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return SecureBeginForm(ajaxHelper, taskResult.Result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginForm(this AjaxHelper ajaxHelper, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.SecureBeginForm(taskResult.Result, ajaxOptions, htmlAttributes);
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, ActionResult result, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.SecureBeginRouteForm(routeName, result, ajaxOptions, null);
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, ActionResult result, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.SecureBeginRouteForm(routeName, result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, ActionResult result, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.BeginRouteForm(routeName, result.GetRouteValueDictionary(), ajaxOptions, htmlAttributes);
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions)
        {
            return ajaxHelper.SecureBeginRouteForm(routeName, taskResult.Result, ajaxOptions, null);
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return ajaxHelper.SecureBeginRouteForm(routeName, taskResult.Result, ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm SecureBeginRouteForm(this AjaxHelper ajaxHelper, string routeName, Task<ActionResult> taskResult, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            return ajaxHelper.SecureBeginRouteForm(routeName, taskResult.Result, ajaxOptions, htmlAttributes);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result)
        {
            return MapRoute(routes, name, url, result, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, object defaults)
        {
            return MapRoute(routes, name, url, result, defaults, null /*constraints*/, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, string[] namespaces)
        {
            return MapRoute(routes, name, url, result, null /*defaults*/, namespaces);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, object defaults, object constraints)
        {
            return MapRoute(routes, name, url, result, defaults, constraints, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, object defaults, string[] namespaces)
        {
            return MapRoute(routes, name, url, result, defaults, null /*constraints*/, namespaces);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, ActionResult result, object defaults, object constraints, string[] namespaces)
        {
            // Create and add the route
            var route = CreateRoute(url, result, defaults, constraints, namespaces);
            routes.Add(name, route);
            return route;
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult)
        {
            return MapRoute(routes, name, url, taskResult.Result, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult, object defaults)
        {
            return MapRoute(routes, name, url, taskResult.Result, defaults, null /*constraints*/, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult, string[] namespaces)
        {
            return MapRoute(routes, name, url, taskResult.Result, null /*defaults*/, namespaces);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult, object defaults, object constraints)
        {
            return MapRoute(routes, name, url, taskResult.Result, defaults, constraints, null /*namespaces*/);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult, object defaults, string[] namespaces)
        {
            return MapRoute(routes, name, url, taskResult.Result, defaults, null /*constraints*/, namespaces);
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, Task<ActionResult> taskResult, object defaults, object constraints, string[] namespaces)
        {
            return routes.MapRoute(name, url, taskResult.Result, defaults, constraints, namespaces);
        }

        // Note: can't name the AreaRegistrationContext methods 'MapRoute', as that conflicts with the existing methods
        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result)
        {
            return MapRouteArea(context, name, url, result, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result, object defaults)
        {
            return MapRouteArea(context, name, url, result, defaults, null /*constraints*/, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result, string[] namespaces)
        {
            return MapRouteArea(context, name, url, result, null /*defaults*/, namespaces);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result, object defaults, object constraints)
        {
            return MapRouteArea(context, name, url, result, defaults, constraints, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result, object defaults, string[] namespaces)
        {
            return MapRouteArea(context, name, url, result, defaults, null /*constraints*/, namespaces);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, ActionResult result, object defaults, object constraints, string[] namespaces)
        {
            // Create and add the route
            if ((namespaces == null) && (context.Namespaces != null))
            {
                namespaces = context.Namespaces.ToArray();
            }
            var route = CreateRoute(url, result, defaults, constraints, namespaces);
            context.Routes.Add(name, route);
            route.DataTokens["area"] = context.AreaName;
            bool useNamespaceFallback = (namespaces == null) || (namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = useNamespaceFallback;
            return route;
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult)
        {
            return MapRouteArea(context, name, url, taskResult.Result, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult, object defaults)
        {
            return MapRouteArea(context, name, url, taskResult.Result, defaults, null /*constraints*/, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult, string[] namespaces)
        {
            return MapRouteArea(context, name, url, taskResult.Result, null /*defaults*/, namespaces);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult, object defaults, object constraints)
        {
            return MapRouteArea(context, name, url, taskResult.Result, defaults, constraints, null /*namespaces*/);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult, object defaults, string[] namespaces)
        {
            return MapRouteArea(context, name, url, taskResult.Result, defaults, null /*constraints*/, namespaces);
        }

        public static Route MapRouteArea(this AreaRegistrationContext context, string name, string url, Task<ActionResult> taskResult, object defaults, object constraints, string[] namespaces)
        {

            return context.MapRouteArea(name, url, taskResult.Result, defaults, constraints, namespaces);
        }

        private static Route CreateRoute(string url, ActionResult result, object defaults, object constraints, string[] namespaces)
        {
            // Start by adding the default values from the anonymous object (if any)
            var routeValues = new RouteValueDictionary(defaults);

            // Then add the Controller/SecureAction names and the parameters from the call
            foreach (var pair in result.GetRouteValueDictionary())
            {
                routeValues.Add(pair.Key, pair.Value);
            }

            var routeConstraints = new RouteValueDictionary(constraints);

            // Create and add the route
            var route = new Route(url, routeValues, routeConstraints, new MvcRouteHandler());

            route.DataTokens = new RouteValueDictionary();

            if (namespaces != null && namespaces.Length > 0)
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            return route;
        }

        public static IT4MVCActionResult GetT4MVCResult(this ActionResult result)
        {
            var t4MVCResult = result as IT4MVCActionResult;
            if (t4MVCResult == null)
            {
                throw new InvalidOperationException("T4MVC was called incorrectly. You may need to force it to regenerate by right clicking on T4MVC.tt and choosing Run Custom Tool");
            }
            return t4MVCResult;
        }

        public static RouteValueDictionary GetRouteValueDictionary(this ActionResult result)
        {
            return result.GetT4MVCResult().RouteValueDictionary;
        }

        public static ActionResult SecureAddRouteValues(this ActionResult result, object routeValues)
        {
            return result.SecureAddRouteValues(new RouteValueDictionary(routeValues));
        }

        public static ActionResult SecureAddRouteValues(this ActionResult result, RouteValueDictionary routeValues)
        {
            RouteValueDictionary currentRouteValues = result.GetRouteValueDictionary();

            // Add all the extra values
            foreach (var pair in routeValues)
            {
                ModelUnbinderHelpers.AddRouteValues(currentRouteValues, pair.Key, pair.Value);
            }

            return result;
        }

        public static ActionResult SecureAddRouteValues(this ActionResult result, System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            // Copy all the values from the NameValueCollection into the route dictionary
            if (nameValueCollection.AllKeys.Any(m => m == null))  //if it has a null, the CopyTo extension will crash!
            {
                var filtered = new System.Collections.Specialized.NameValueCollection(nameValueCollection);
                filtered.Remove(null);
                filtered.CopyTo(result.GetRouteValueDictionary());
            }
            else
            {
                nameValueCollection.CopyTo(result.GetRouteValueDictionary(), replaceEntries: true);
            }
            return result;
        }

        public static ActionResult AddRouteValue(this ActionResult result, string name, object value)
        {
            RouteValueDictionary routeValues = result.GetRouteValueDictionary();
            ModelUnbinderHelpers.AddRouteValues(routeValues, name, value);
            return result;
        }

        public static void InitMVCT4Result(this IT4MVCActionResult result, string area, string controller, string secureAction, string protocol = null)
        {
            result.Controller = controller;
            result.Action = secureAction;
            result.Protocol = protocol;
            result.RouteValueDictionary = new RouteValueDictionary();
            result.RouteValueDictionary.Add("Area", area ?? "");
            result.RouteValueDictionary.Add("Controller", controller);
            result.RouteValueDictionary.Add("SecureAction", secureAction);
        }

        public static bool FileExists(string virtualPath)
        {
            if (!HostingEnvironment.IsHosted) return false;
            string filePath = HostingEnvironment.MapPath(virtualPath);
            return System.IO.File.Exists(filePath);
        }

        static DateTime CenturySecureBegin = new DateTime(2001, 1, 1);
        public static string TimestampString(string virtualPath)
        {
            if (!HostingEnvironment.IsHosted) return string.Empty;
            string filePath = HostingEnvironment.MapPath(virtualPath);
            return Convert.ToString((System.IO.File.GetLastWriteTimeUtc(filePath).Ticks - CenturySecureBegin.Ticks) / 1000000000, 16);
        }
    }
}

