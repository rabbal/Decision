using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace Decision.Common.Controller
{
    /// <summary>
    /// protected tempdata with machinkey of form authentication 
    /// </summary>
    public class SecureTempDataProvider : ITempDataProvider
    {
        public static event EventHandler<Exception> ValidationException;

        const string AnonymousCookieValuePrefix = "_";
        const string AuthenticatedCookieValuePrefix = ".";
        const string CookieName = "TempData";
        const string MachineKeyPurpose = "CookieTempDataProvider:{0}";
        const string Anonymous = "<anonymous>";

        public void SaveTempData(
            ControllerContext controllerContext,
            IDictionary<string, object> values)
        {
            var bytes = SerializeWithBinaryFormatter(values);
            bytes = Compress(bytes);
            var value = Protect(bytes, controllerContext.HttpContext);
            IssueCookie(controllerContext, value);
        }

        public IDictionary<string, object> LoadTempData(
            ControllerContext controllerContext)
        {
            var value = GetCookieValue(controllerContext);
            var bytes = Unprotect(value, controllerContext.HttpContext);
            if (bytes == null && value != null)
            {
                // failure, so remove cookie
                IssueCookie(controllerContext, null);
                return null;
            }
            bytes = Decompress(bytes);
            return DeserializeWithBinaryFormatter(bytes);
        }

        static string GetCookieValue(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName)) return null;
            var c = controllerContext.HttpContext.Request.Cookies[CookieName];
            return c != null ? c.Value : null;
        }

        static void IssueCookie(ControllerContext controllerContext, string value)
        {
            // if we don't have a value and there's no prior cookie then exit
            if (value == null && !controllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName)) return;

            var cookie = new HttpCookie(CookieName, value)
            {
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

        static string GetAnonMachineKeyPurpose()
        {
            return String.Format(MachineKeyPurpose, Anonymous);
        }

        static string GetMachineKeyPurpose(HttpContextBase ctx)
        {
            if (ctx.User == null || ctx.User.Identity == null || !ctx.User.Identity.IsAuthenticated) return GetAnonMachineKeyPurpose();
            return String.Format(MachineKeyPurpose, ctx.User.Identity == null ? "" : ctx.User.Identity.Name);
        }

        static string GetMachineKeyPurposeFromPrefix(string prefix, HttpContextBase ctx)
        {
            if (prefix == AnonymousCookieValuePrefix)
            {
                return GetAnonMachineKeyPurpose();
            }
            if (prefix == AuthenticatedCookieValuePrefix && ctx.User.Identity.IsAuthenticated)
            {
                return String.Format(MachineKeyPurpose, ctx.User.Identity.Name);
            }
            return null;
        }

        static string GetMachineKeyPrefix(HttpContextBase ctx)
        {
            if (ctx.User == null || ctx.User.Identity == null) return AnonymousCookieValuePrefix;

            return (ctx.User.Identity.IsAuthenticated) ?
                AuthenticatedCookieValuePrefix :
                AnonymousCookieValuePrefix;
        }

        static string Protect(byte[] data, HttpContextBase ctx)
        {
            if (data == null || data.Length == 0) return null;

            var purpose = GetMachineKeyPurpose(ctx);
            var value = MachineKey.Protect(data, purpose);

            var prefix = GetMachineKeyPrefix(ctx);
            return prefix + Convert.ToBase64String(value);
        }

        byte[] Unprotect(string value, HttpContextBase ctx)
        {
            if (String.IsNullOrWhiteSpace(value)) return null;

            var prefix = value[0].ToString(CultureInfo.InvariantCulture);
            var purpose = GetMachineKeyPurposeFromPrefix(prefix, ctx);
            if (purpose == null) return null;

            value = value.Substring(1);
            var bytes = Convert.FromBase64String(value);
            try
            {
                return MachineKey.Unprotect(bytes, purpose);
            }
            catch (CryptographicException ex)
            {
                if (ValidationException != null)
                {
                    ValidationException(this, ex);
                }
                return null;
            }
        }

        static byte[] Compress(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            using (var input = new MemoryStream(data))
            {
                using (var output = new MemoryStream())
                {
                    using (Stream cs = new DeflateStream(output, CompressionMode.Compress))
                    {
                        input.CopyTo(cs);
                    }

                    return output.ToArray();
                }
            }
        }

        static byte[] Decompress(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            using (var input = new MemoryStream(data))
            {
                using (var output = new MemoryStream())
                {
                    using (Stream cs = new DeflateStream(input, CompressionMode.Decompress))
                    {
                        cs.CopyTo(output);
                    }

                    var result = output.ToArray();
                    return result;
                }
            }
        }

        static byte[] SerializeWithBinaryFormatter(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            var f = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                f.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                return ms.ToArray();
            }
        }

        static IDictionary<string, object> DeserializeWithBinaryFormatter(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            var f = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var obj = f.Deserialize(ms);
                return obj as IDictionary<string, object>;
            }
        }

        string SerializeWithJsonFormatter(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        IDictionary<string, object> DeserializeWithJsonFormatter(string data)
        {
            return string.IsNullOrEmpty(data) ? null : JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        }
    }
}
