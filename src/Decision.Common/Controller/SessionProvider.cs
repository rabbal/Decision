using System.Web;
using Decision.Common.Extentions;

namespace Decision.Common.Controller
{
    public class SessionProvider : ISessionProvider
    {
        private readonly HttpContextBase _httpContextBase;
        public SessionProvider(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public object Get(string key)
        {
            return null;
        }

        public T Get<T>(string key) where T : class
        {
            return CookieExtention.DeserializeCookie<T>(_httpContextBase.GetCookieValue(key));
        }

        public void Remove(string key)
        {
            _httpContextBase.RemoveCookie(key);
        }

        public void RemoveAll()
        {
            _httpContextBase.RemoveAllCookies();
        }

        public void Store<T>(string key, T value) where T : class
        {
            _httpContextBase.AddCookie(key, CookieExtention.SerializeToBase64EncodedString(value));
        }


    }
}
