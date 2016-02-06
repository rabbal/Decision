using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Extentions;

namespace Decision.Common.Controller
{
    public class CookieTempDataProvider : ITempDataProvider
    {
        #region Fields
        private const string TempDataCookieKey = "__ControllerTempData";
        private readonly IRemotingFormatter _formatter;
        #endregion

        #region Ctor
        public CookieTempDataProvider(IRemotingFormatter formatter)
        {
            _formatter = formatter;
        }

        #endregion

        #region LoadTempData
        protected virtual IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            var cookie = GetCookie(controllerContext);
            if (cookie == null || !cookie.Value.IsNotEmpty())
            {
                IssueCookie(controllerContext, null);
                return new Dictionary<string, object>();
            }

            var deserializedDictionary = Base64StringToDictionary(cookie.Value);

            return deserializedDictionary;
        }
        IDictionary<string, object> ITempDataProvider.LoadTempData(ControllerContext controllerContext)
        {
            return LoadTempData(controllerContext);
        }
        #endregion

        #region SaveTempData
        protected virtual void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            var cookieValue = DictionaryToBase64String(values);

            IssueCookie(controllerContext, cookieValue);
        }
        void ITempDataProvider.SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            SaveTempData(controllerContext, values);
        }
        #endregion

        #region Base64StringToDictionary
        private IDictionary<string, object> Base64StringToDictionary(string base64EncodedSerializedTempData)
        {
            var bytes = Convert.FromBase64String(base64EncodedSerializedTempData);
            using (var memStream = new MemoryStream(bytes))
            {
                return _formatter.Deserialize(memStream, null) as IDictionary<string, object>;
            }
        }
        #endregion

        #region DictionaryToBase64String
        private string DictionaryToBase64String(IDictionary<string, object> values)
        {
            using (var memStream = new MemoryStream())
            {
                memStream.Seek(0, SeekOrigin.Begin);
                _formatter.Serialize(memStream, values);
                memStream.Seek(0, SeekOrigin.Begin);
                var bytes = memStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
        #endregion

        #region IssuCookie
        static void IssueCookie(ControllerContext controllerContext, string value)
        {
            // if we don't have a value and there's no prior cookie then exit
            if (value == null && !controllerContext.HttpContext.Request.Cookies.AllKeys.Contains(TempDataCookieKey)) return;

            var cookie = new HttpCookie(TempDataCookieKey, value)
            {
                Expires = DateTime.MinValue,
                // don't allow javascript access to the cookie
                HttpOnly = true,
                // set the path so other apps on the same server don't see the cookie
                Path = controllerContext.HttpContext.Request.ApplicationPath,
                // ideally we're always going over SSL, but be flexible for non-SSL apps
                Secure = controllerContext.HttpContext.Request.IsSecureConnection
            };

            if (value == null)
            {
                // if we have no data then issue an expired cookie to clear the cookie
                cookie.Expires = DateTime.Now.AddMonths(-1);
            }

            controllerContext.HttpContext.Response.Cookies.Add(cookie);
        }

        private static HttpCookie GetCookie(ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.Request.Cookies[TempDataCookieKey];
        }
        #endregion

    }
}
