using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public static class HttpRequestMessageExtensions
    {
        public static void Add(this HttpRequestMessage request, string key, object o)
        {
            request.Properties[key] = o;
        }
        public static T Get<T>(this HttpRequestMessage request, string key)
        {
            object result;
            if (!request.Properties.TryGetValue(key, out result)) return default(T);

            if (result is T)
            {
                return (T)result;
            }
            try
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (InvalidCastException e)
            {
                throw new InvalidCastException("invalid casting when retrieve key's result from web api httpRequest's properties",e);
            }
        }
        public static bool IsAjaxRequest(this HttpRequestMessage request)
        {
            IEnumerable<string> headers;
            if (!request.Headers.TryGetValues("X-Requested-With", out headers)) return false;
            var header = headers.FirstOrDefault();
            if (!string.IsNullOrEmpty(header))
            {
                return header.ToLowerInvariant() == "xmlhttprequest";
            }
            return false;
        }
    }
}