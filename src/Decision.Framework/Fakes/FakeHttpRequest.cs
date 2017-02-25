using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Decision.Framework.Fakes
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection _cookies;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly string _relativeUrl;
        private readonly NameValueCollection _serverVariables;
        private readonly Uri _url;
        private readonly Uri _urlReferrer;

        public FakeHttpRequest(string relativeUrl, Uri url, Uri urlReferrer)
            : this(relativeUrl, HttpVerbs.Get.ToString("g"), url, urlReferrer, null, null, null, null)
        {
        }

        public FakeHttpRequest(string relativeUrl,
            string method,
            NameValueCollection formParams,
            NameValueCollection queryStringParams,
            HttpCookieCollection cookies,
            NameValueCollection serverVariables)
        {
            HttpMethod = method;
            _relativeUrl = relativeUrl;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _serverVariables = serverVariables;
            //ensure collections are not null
            if (_formParams == null)
                _formParams = new NameValueCollection();
            if (_queryStringParams == null)
                _queryStringParams = new NameValueCollection();
            if (_cookies == null)
                _cookies = new HttpCookieCollection();
            if (_serverVariables == null)
                _serverVariables = new NameValueCollection();
        }


        public FakeHttpRequest(string relativeUrl,
            string method,
            Uri url,
            Uri urlReferrer,
            NameValueCollection formParams,
            NameValueCollection queryStringParams,
            HttpCookieCollection cookies,
            NameValueCollection serverVariables)
            : this(relativeUrl, method, formParams, queryStringParams, cookies, serverVariables)
        {
            _url = url;
            _urlReferrer = urlReferrer;
        }

        public override NameValueCollection ServerVariables => _serverVariables;

        public override NameValueCollection Form => _formParams;

        public override NameValueCollection QueryString => _queryStringParams;

        public override HttpCookieCollection Cookies => _cookies;

        public override string AppRelativeCurrentExecutionFilePath => _relativeUrl;

        public override Uri Url => _url ?? new Uri("http://tempuri.org");

        public override Uri UrlReferrer => _urlReferrer ?? new Uri("http://tempuri.org");

        public override string PathInfo => "";

        public override string ApplicationPath
        {
            get
            {
                //we know that relative paths always start with ~/
                //ApplicationPath should start with /
                if (_relativeUrl != null && _relativeUrl.StartsWith("~/"))
                    return _relativeUrl.Remove(0, 1);
                return null;
            }
        }

        public override string HttpMethod { get; }

        public override string UserHostAddress => null;

        public override string RawUrl => ApplicationPath;

        public override bool IsSecureConnection => false;

        public override bool IsAuthenticated => false;

        public override string[] UserLanguages => new string[] {};
    }
}