using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using NTierMvcFramework.Common.Extensions;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.Email
{
    public class EmailMessage : ICloneable<EmailMessage>
    {
        public EmailMessage()
        {
            BodyFormat = MailBodyFormat.Html;
            Priority = MailPriority.Normal;

            To = new List<EmailAddress>();
            Cc = new List<EmailAddress>();
            Bcc = new List<EmailAddress>();
            ReplyTo = new List<EmailAddress>();

            Attachments = new List<MailAttachment>();

            Headers = new NameValueCollection();
        }

        public EmailMessage(string to, string subject, string body, string from)
            : this()
        {
            Guard.ArgumentNotEmpty(() => to);
            Guard.ArgumentNotEmpty(() => from);
            Guard.ArgumentNotEmpty(() => subject);
            Guard.ArgumentNotEmpty(() => body);

            To.Add(new EmailAddress(to));
            Subject = subject;
            Body = body;
            From = new EmailAddress(from);
        }

        public EmailMessage(EmailAddress to, string subject, string body, EmailAddress from)
            : this()
        {
            Guard.ArgumentNotNull(() => to);
            Guard.ArgumentNotNull(() => from);
            Guard.ArgumentNotEmpty(() => subject);
            Guard.ArgumentNotEmpty(() => body);

            To.Add(to);
            Subject = subject;
            Body = body;
            From = from;
        }

        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AltText { get; set; }

        public MailBodyFormat BodyFormat { get; set; }
        public MailPriority Priority { get; set; }

        public ICollection<EmailAddress> To { get; }
        public ICollection<EmailAddress> Cc { get; }
        public ICollection<EmailAddress> Bcc { get; }
        public ICollection<EmailAddress> ReplyTo { get; }

        public ICollection<MailAttachment> Attachments { get; }

        public NameValueCollection Headers { get; }

        public void AddAttachment(MailAttachment attachment)
        {
            Attachments.Add(attachment);
        }

        public async void BodyFromFile(string filePathOrUrl)
        {
            StreamReader sr = null;

            if (filePathOrUrl.ToLower().StartsWith("http"))
            {
                using (var wc = new WebClient())
                {
                    sr = new StreamReader(await wc.OpenReadTaskAsync(filePathOrUrl));
                }
            }
            else
            {
                sr = new StreamReader(filePathOrUrl, Encoding.Default);
            }

            Body = await sr.ReadToEndAsync();

            sr.Close();
            sr.Dispose();
        }

        #region ICloneable Members

        public EmailMessage Clone()
        {
            var clone = new EmailMessage();

            clone.Attachments.AddRange(Attachments);
            clone.To.AddRange(To);
            clone.Cc.AddRange(Cc);
            clone.Bcc.AddRange(Bcc);
            clone.ReplyTo.AddRange(ReplyTo);
            clone.Headers.AddRange(Headers);

            clone.AltText = AltText;
            clone.Body = Body;
            clone.BodyFormat = BodyFormat;
            clone.From = From;
            clone.Priority = Priority;
            clone.Subject = Subject;

            return clone;
        }

        object ICloneable.Clone() => Clone();

        #endregion
    }

    public enum MailBodyFormat
    {
        Text,
        Html
    }
}