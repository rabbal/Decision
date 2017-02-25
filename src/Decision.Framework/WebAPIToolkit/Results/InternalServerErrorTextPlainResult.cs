using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Decision.Framework.WebAPIToolkit.Results
{
    public class InternalServerErrorTextPlainResult : IHttpActionResult
    {
        public InternalServerErrorTextPlainResult(string content, Encoding encoding, HttpRequestMessage request)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Content = content;
            Encoding = encoding;
            Request = request;
        }

        public string Content { get; private set; }

        public Encoding Encoding { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                RequestMessage = Request,
                Content = new StringContent(Content, Encoding)
            };
            return response;
        }
    }
}