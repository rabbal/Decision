using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace NTierMvcFramework.Common.WebAPIToolkit.Controller
{
    public static class ControllerExtensions
    {
        /// <summary>
        ///     stream the file to response
        /// </summary>
        /// <param name="controller">controller instance</param>
        /// <param name="path">full path of file</param>
        /// <param name="attachment"></param>
        /// <param name="contentType">content type of file</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static HttpResponseMessage File(ApiController controller, string path, bool attachment = false,
            string contentType = null, string fileName = null)
        {
            if (!System.IO.File.Exists(path))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            // Find the MIME type
            //string mimeType = _extensions[Path.GetExtension(path)];
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType ?? "application/octet-stream");

            if (attachment)
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(nameof(attachment));

            if (!string.IsNullOrEmpty(fileName))
                result.Content.Headers.ContentDisposition.FileName = fileName;
            return result;
        }
    }
}