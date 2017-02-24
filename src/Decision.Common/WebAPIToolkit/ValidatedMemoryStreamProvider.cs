using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public class ValidatedMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        private static string[] extensions = new[] { "jpg", "gif", "png" };
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            if (filename.IndexOf('.') < 0)
                return Stream.Null;
            var extension = filename.Split('.').Last();
            return extensions.Any(i => i.Equals(extension, StringComparison.
                InvariantCultureIgnoreCase)) ? base.GetStream(parent, headers) : Stream.Null;
        }
    }
}