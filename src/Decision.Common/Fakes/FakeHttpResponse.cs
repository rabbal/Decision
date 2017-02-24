using System.Text;
using System.Web;

namespace Decision.Common.Fakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private readonly StringBuilder _outputString = new StringBuilder();

        public FakeHttpResponse()
        {
            Cookies = new HttpCookieCollection();
        }

        public string ResponseOutput => _outputString.ToString();

        public override int StatusCode { get; set; }

        public override string RedirectLocation { get; set; }

        public override HttpCookieCollection Cookies { get; }

        public override void Write(string s)
        {
            _outputString.Append(s);
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }
    }
}