using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace Decision.Common.MvcToolkit.Providers
{
    public class CookieTempDataProvider : ITempDataProvider
    {
        public static event EventHandler<Exception> ValidationException;

        private const string AnonymousCookieValuePrefix = "_";
        private const string AuthenticatedCookieValuePrefix = ".";
        private const string CookieName = "TempData";
        private const string MachineKeyPurpose = "CookieTempDataProvider:{0}";
        private const string Anonymous = "<anonymous>";

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
            else
            {
                bytes = Decompress(bytes);
                return DeserializeWithBinaryFormatter(bytes);
            }
        }

        private static string GetCookieValue(ControllerContext controllerContext)
        {
            if (!controllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName)) return null;
            var c = controllerContext.HttpContext.Request.Cookies[CookieName];
            return c?.Value;
        }

        private static void IssueCookie(ControllerContext controllerContext, string value)
        {
            // if we don't have a value and there's no prior cookie then exit
            if (value == null && !controllerContext.HttpContext.Request.Cookies.AllKeys.Contains(CookieName)) return;

            var c = new HttpCookie(CookieName, value)
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
                c.Expires = DateTime.Now.AddMonths(-1);
            }

            controllerContext.HttpContext.Response.Cookies.Add(c);
        }

        private static string GetAnonMachineKeyPurpose()
        {
            return string.Format(MachineKeyPurpose, Anonymous);
        }

        private string GetMachineKeyPurpose(HttpContextBase ctx)
        {
            if (ctx.User == null || ctx.User.Identity == null || !ctx.User.Identity.IsAuthenticated) return GetAnonMachineKeyPurpose();
            return string.Format(MachineKeyPurpose, ctx.User.Identity == null ? "" : ctx.User.Identity.Name);
        }

        private string GetMachineKeyPurposeFromPrefix(string prefix, HttpContextBase ctx)
        {
            if (prefix == AnonymousCookieValuePrefix)
            {
                return GetAnonMachineKeyPurpose();
            }
            if (prefix == AuthenticatedCookieValuePrefix && ctx.User.Identity.IsAuthenticated)
            {
                return string.Format(MachineKeyPurpose, ctx.User.Identity.Name);
            }
            return null;
        }

        private static string GetMachineKeyPrefix(HttpContextBase ctx)
        {
            if (ctx.User?.Identity == null) return AnonymousCookieValuePrefix;

            return (ctx.User.Identity.IsAuthenticated) ?
                AuthenticatedCookieValuePrefix :
                AnonymousCookieValuePrefix;
        }

        private string Protect(byte[] data, HttpContextBase ctx)
        {
            if (data == null || data.Length == 0) return null;

            var purpose = GetMachineKeyPurpose(ctx);
            var value = MachineKey.Protect(data, purpose);

            var prefix = GetMachineKeyPrefix(ctx);
            return prefix + Convert.ToBase64String(value);
        }

        private byte[] Unprotect(string value, HttpContextBase ctx)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            var prefix = value[0].ToString();
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
                ValidationException?.Invoke(this, ex);
                return null;
            }
        }

        private static byte[] Compress(byte[] data)
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

        private static byte[] Decompress(byte[] data)
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

        private static byte[] SerializeWithBinaryFormatter(IDictionary<string, object> data)
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

        private static IDictionary<string, object> DeserializeWithBinaryFormatter(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            var f = new BinaryFormatter();
            using (var ms = new MemoryStream(data))
            {
                var obj = f.Deserialize(ms);
                return obj as IDictionary<string, object>;
            }
        }

        private static string SerializeWithJsonFormatter(IDictionary<string, object> data)
        {
            if (data == null || data.Keys.Count == 0) return null;

            return JsonConvert.SerializeObject(data);
        }

        private static IDictionary<string, object> DeserializeWithJsonFormatter(string data)
        {
            return string.IsNullOrEmpty(data) ? null : JsonConvert.DeserializeObject<IDictionary<string, object>>(data);
        }
    }
}