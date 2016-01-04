using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Decision.Common.HtmlCleaner
{
    public class AntiXssModule : IHttpModule
    {
        #region Fields (3)

        private static readonly Regex CleanAllTags = new Regex("<[^>]+>", RegexOptions.Compiled);

        private static readonly IList<string> IgnoreList = new List<string>
        {
            "__EVENTVALIDATION",
            "__LASTFOCUS",
            "__EVENTTARGET",
            "__EVENTARGUMENT",
            "__VIEWSTATE",
            "__SCROLLPOSITIONX",
            "__SCROLLPOSITIONY",
            "__VIEWSTATEENCRYPTED",
            "__ASYNCPOST",
            "pagedata" //custom
        };

        //اندكي دستكاري در سيستم داخلي دات نت
        private static readonly PropertyInfo ReadonlyProperty = typeof(NameValueCollection).GetProperty("IsReadOnly",
            BindingFlags.Instance | BindingFlags.NonPublic);

        #endregion Fields

        #region Methods (6)

        // Public Methods (2) 

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += CleanUpInput;
        }

        // Private Methods (4) 

        private static void CleanUpAndEncodeCookies(HttpCookieCollection cookiesCollection)
        {
            foreach (var key in cookiesCollection.AllKeys)
            {
                var cookie = cookiesCollection[key];
                if (cookie == null) continue;

                foreach (var cookieKey in cookie.Values.AllKeys)
                {
                    var origData = cookie.Values[cookieKey];
                    if (string.IsNullOrEmpty(origData)) continue;
                    origData = origData.Trim();

                    //در حالت كوكي‌ها دليلي براي ارسال هيچ نوع تگي وجود ندارد
                    var modifiedData = HttpUtility.HtmlEncode(CleanAllTags.Replace(origData, string.Empty));
                    if (origData != modifiedData)
                    {
                        //todo: log this attack...
                        cookie.Values[cookieKey] = modifiedData;
                    }
                }
            }
        }

        private static void CleanUpAndEncodeFormFields(NameValueCollection formFieldsCollection)
        {
            ReadonlyProperty.SetValue(formFieldsCollection, false, null); //IsReadOnly=false

            foreach (var key in formFieldsCollection.AllKeys)
            {
                var origData = formFieldsCollection[key];
                if (string.IsNullOrEmpty(origData)) continue;
                origData = origData.Trim();

                //قصد تميز سازي يك سري از موارد را نداريم چون در اين حالت وب فرم‌ها از كار مي‌افتند
                if (IgnoreList.Contains(key)) continue;
                //در ساير موارد كاربران مجازند فقط تگ‌هاي سالم را ارسال كنند و مابقي حذف مي‌شود
                var modifiedData = origData.ToSafeHtml();
                if (origData != modifiedData)
                {
                    //todo: log this attack...                                      
                    formFieldsCollection[key] = modifiedData;
                }
            }

            ReadonlyProperty.SetValue(formFieldsCollection, true, null); //IsReadOnly=true
        }

        private static void CleanUpAndEncodeQueryStrings(NameValueCollection queryStringsCollection)
        {
            ReadonlyProperty.SetValue(queryStringsCollection, false, null); //IsReadOnly=false

            foreach (var key in queryStringsCollection.AllKeys)
            {
                var origData = queryStringsCollection[key];
                if (string.IsNullOrEmpty(origData)) continue;
                origData = origData.Trim();

                //در حالت كوئري استرينگ دليلي براي ارسال هيچ نوع تگي وجود ندارد
                var modifiedData = HttpUtility.HtmlEncode(CleanAllTags.Replace(origData, string.Empty));
                if (origData != modifiedData)
                {
                    //todo: log this attack...
                    queryStringsCollection[key] = modifiedData;
                }
            }

            ReadonlyProperty.SetValue(queryStringsCollection, true, null); //IsReadOnly=true
        }

        private static void CleanUpInput(object sender, EventArgs e)
        {
            var request = ((HttpApplication)sender).Request;

            CleanUpAndEncodeQueryStrings(request.QueryString);

            if (request.HttpMethod == "POST")
            {
                CleanUpAndEncodeFormFields(request.Form);
            }

            CleanUpAndEncodeCookies(request.Cookies);
        }

        #endregion Methods
    }
}