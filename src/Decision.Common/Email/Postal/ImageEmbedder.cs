using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Decision.Common.Email.Postal
{
    public class ImageEmbedder
    {
        internal static string ViewDataKey = "Postal.ImageEmbedder";
        public ImageEmbedder()
        {
            createLinkedResource = CreateLinkedResource;
        }

        public ImageEmbedder(Func<string, LinkedResource> createLinkedResource)
        {
            this.createLinkedResource = createLinkedResource;
        }

        private readonly Func<string, LinkedResource> createLinkedResource;
        private readonly Dictionary<string, LinkedResource> images = new Dictionary<string, LinkedResource>();

        public bool HasImages => images.Count > 0;

        public static LinkedResource CreateLinkedResource(string imagePathOrUrl)
        {
            if (Uri.IsWellFormedUriString(imagePathOrUrl, UriKind.Absolute))
            {
                using (var client = new WebClient())
                {
                    var bytes = client.DownloadData(imagePathOrUrl);
                    return new LinkedResource(new MemoryStream(bytes));
                }
            }
            else
            {
                return new LinkedResource(File.OpenRead(imagePathOrUrl));
            }
        }
        public LinkedResource ReferenceImage(string imagePathOrUrl, string contentType = null)
        {
            LinkedResource resource;
            if (images.TryGetValue(imagePathOrUrl, out resource)) return resource;

            resource = createLinkedResource(imagePathOrUrl);

            contentType = contentType ?? DetermineContentType(imagePathOrUrl);
            if (contentType != null)
            {
                resource.ContentType = new ContentType(contentType);
            }

            images[imagePathOrUrl] = resource;
            return resource;
        }

        private static string DetermineContentType(string pathOrUrl)
        {
            if (pathOrUrl == null) throw new ArgumentNullException(nameof(pathOrUrl));

            var extension = Path.GetExtension(pathOrUrl).ToLowerInvariant();
            switch (extension)
            {
                case ".png":
                    return "image/png";
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                default:
                    return null;
            }
        }
        public void AddImagesToView(AlternateView view)
        {
            foreach (var image in images)
            {
                view.LinkedResources.Add(image.Value);
            }
        }

    }
}
