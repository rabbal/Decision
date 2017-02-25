using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

namespace Decision.Framework.Net.Mail.Postal
{
    /// <summary>
    /// Renders a preview of an email to display in the browser.
    /// </summary>
    public class EmailViewResult : ViewResult
    {
        private const string TextContentType = "text/plain";
        private const string HtmlContentType = "text/html";

        private IEmailViewRenderer Renderer { get; set; }
        private IEmailParser Parser { get; set; }
        private Email Email { get; set; }

        /// <summary>
        /// Creates a new <see cref="EmailViewResult"/>.
        /// </summary>
        public EmailViewResult(Email email, IEmailViewRenderer renderer, IEmailParser parser)
        {
            Email = email;
            Renderer = renderer ?? new EmailViewRenderer(ViewEngineCollection);
            Parser = parser ?? new EmailParser(Renderer);
        }

        /// <summary>
        /// Creates a new <see cref="EmailViewResult"/>.
        /// </summary>
        public EmailViewResult(Email email)
            : this(email, null, null)
        {
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.RequestContext.HttpContext;
            var query = httpContext.Request.QueryString;
            var format = query["format"];
            var contentType = ExecuteResult(context.HttpContext.Response.Output, format);
            httpContext.Response.ContentType = contentType;
        }

        public string ExecuteResult(TextWriter writer, string format = null)
        {
            var result = Renderer.Render(Email);
            var mailMessage = Parser.Parse(result, Email);

            // no special requests; render what's in the template
            if (string.IsNullOrEmpty(format))
            {
                if (!mailMessage.IsBodyHtml)
                {
                    writer.Write(result);
                    return TextContentType;
                }

                var template = Extract(result);
                template.Write(writer);
                return HtmlContentType;
            }

            // Check if alternative
            var alternativeContentType = CheckAlternativeViews(writer, mailMessage, format);

            if (!string.IsNullOrEmpty(alternativeContentType))
                return alternativeContentType;

            if (format != "text")
            {
                if (format != "html") throw new NotSupportedException($"Unsupported format {format}");

                if (!mailMessage.IsBodyHtml)
                    throw new NotSupportedException("No html view available for this email");

                var template = Extract(result);
                template.Write(writer);
                return HtmlContentType;
            }

            if(mailMessage.IsBodyHtml)
                throw new NotSupportedException("No text view available for this email");

            writer.Write(result);
            return TextContentType;
        }

        private static string CheckAlternativeViews(TextWriter writer, MailMessage mailMessage, string format)
        {
            var contentType = format == "html"
                ? HtmlContentType
                : TextContentType;

            // check for alternative view
            var view = mailMessage.AlternateViews.FirstOrDefault(v => v.ContentType.MediaType == contentType);

            if (view == null)
                return null;

            string content;
            using (var reader = new StreamReader(view.ContentStream))
                content = reader.ReadToEnd();

            content = ReplaceLinkedImagesWithEmbeddedImages(view, content);

            writer.Write(content);
            return contentType;
        }

        private class TemplateParts
        {
            private readonly string header;
            private readonly string body;

            public TemplateParts(string header, string body)
            {
                this.header = header;
                this.body = body;
            }

            public void Write(TextWriter writer)
            {
                writer.WriteLine("<!--");
                writer.WriteLine(header);
                writer.WriteLine("-->");
                writer.WriteLine(body);
            }
        }

        private static TemplateParts Extract(string template)
        {
            var headerBuilder = new StringBuilder();

            using (var reader = new StringReader(template))
            {
                // try to read until we passed headers
                var line = reader.ReadLine();

                while (line != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        return new TemplateParts(headerBuilder.ToString(), reader.ReadToEnd());
                    }

                    headerBuilder.AppendLine(line);
                    line = reader.ReadLine();
                }
            }

            return null;
        }

        internal static string ReplaceLinkedImagesWithEmbeddedImages(AlternateView view, string content)
        {
            var resources = view.LinkedResources;

            if (!resources.Any())
                return content;

            foreach (var resource in resources)
            {
                var find = "src=\"cid:" + resource.ContentId + "\"";
                var imageData = ComposeImageData(resource);
                content = content.Replace(find, "src=\"" + imageData + "\"");
            }

            return content;
        }

        private static string ComposeImageData(LinkedResource resource)
        {
            var contentType = resource.ContentType.MediaType;
            var bytes = ReadFully(resource.ContentStream);
            return $"data:{contentType};base64,{Convert.ToBase64String(bytes)}";
        }

        private static byte[] ReadFully(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
