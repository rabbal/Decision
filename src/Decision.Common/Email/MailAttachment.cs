using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using net = System.Net.Mail;

namespace Decision.Common.Email
{
    /// <summary>
    ///     attachment-multipart
    /// </summary>
    public class MailAttachment
    {
        //private readonly net.Attachment _attachment;

        public MailAttachment()
        {
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(""));
            Stream = stream;

            Instance = new net.Attachment(Stream, "");
        }

        public MailAttachment(string filePath)
        {
            Instance = new net.Attachment(filePath);
            GetContentFromFile(filePath);
        }

        public MailAttachment(string filePath, string mediaType)
        {
            Instance = new net.Attachment(filePath, mediaType);
        }

        public ContentType ContentType
        {
            get { return Instance.ContentType; }
            set { Instance.ContentType = value; }
        }

        public string Name
        {
            get { return Instance.Name; }
            set { Instance.Name = value; }
        }

        public string MediaType
        {
            get { return Instance.ContentType.MediaType; }
            set { Instance.ContentType.MediaType = value; }
        }

        public ContentDisposition ContentDisposition => Instance.ContentDisposition;

        public TransferEncoding ContentTransferEncoding
        {
            get { return Instance.TransferEncoding; }
            set { Instance.TransferEncoding = value; }
        }

        public string ContentDescription { get; set; }
        public Stream Stream { get; set; }

        //NEW METHODS TO WRAP ATTACHMENT
        //TODO: Testen
        public net.Attachment Instance { get; set; }

        //public string FileName { get; set; }

        public void GetContentFromFile(string location)
        {
            using (var fileStream = new FileStream(location, FileMode.Open, FileAccess.Read))
            {
                var length = (int) fileStream.Length;
                var buffer = new byte[length];
                int count;
                var sum = 0;

                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;

                Stream = new MemoryStream(buffer);

                var extension = Path.GetExtension(location);
                var filename = Path.GetFileName(location);

                switch (extension)
                {
                    case ".txt":
                        MediaType = "text/plain";
                        break;
                    case ".jpg":
                        MediaType = "image/jpeg";
                        break;
                    default:
                        MediaType = "application/octet-stream";
                        break;
                }

                Name = filename;
                ContentTransferEncoding = TransferEncoding.Base64;
                ContentDisposition.FileName = filename;
                ContentDisposition.DispositionType = "attachment";
            }
        }

        public void GetContentFromString(string content)
        {
            Stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            //this.Stream = new MemoryStream(Convert.FromBase64String(content));   
        }

        public void GetContentFromBase64String(string content)
        {
            Stream = new MemoryStream(Convert.FromBase64String(content));
        }

        public MailAttachment CreateAttachmentFromString(string content, ContentType contentType)
        {
            Instance = net.Attachment.CreateAttachmentFromString(content, contentType);
            return InitAttachment(Instance);
        }

        public MailAttachment CreateAttachmentFromString(string content, string name)
        {
            Instance = net.Attachment.CreateAttachmentFromString(content, name);
            return InitAttachment(Instance);
        }

        public MailAttachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding,
            string mediaType)
        {
            Instance = net.Attachment.CreateAttachmentFromString(content, name, contentEncoding, mediaType);
            return InitAttachment(Instance);
        }

        public MailAttachment InitAttachment(net.Attachment tempAttachment)
        {
            Stream = tempAttachment.ContentStream;
            Name = tempAttachment.Name;
            ContentTransferEncoding = tempAttachment.TransferEncoding;
            ContentType = tempAttachment.ContentType;
            MediaType = tempAttachment.ContentType.MediaType;

            return this;
        }
    }
}