using System.IO;
using System.Web;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public class HttpPostedFileMultipart : HttpPostedFileBase
    {
        private readonly MemoryStream _fileContents;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpPostedFileMultipart" /> class.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the file on the client</param>
        /// <param name="contentType">The MIME content type of an uploaded file</param>
        /// <param name="fileContents">The contents of the uploaded file.</param>
        public HttpPostedFileMultipart(string fileName, string contentType, byte[] fileContents)
        {
            FileName = fileName;
            ContentType = contentType;
            _fileContents = new MemoryStream(fileContents);
        }

        public override int ContentLength => (int)_fileContents.Length;
        public override string ContentType { get; }
        public override string FileName { get; }
        public override Stream InputStream => _fileContents;
    }
}