using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.Email.Postal
{
    public class EmailService : IEmailService
    {
        public EmailService(ViewEngineCollection viewEngines)
        {
            emailViewRenderer = new EmailViewRenderer(viewEngines);
            emailParser = new EmailParser(emailViewRenderer);
            this.createSmtpClient = createSmtpClient ?? (() => new SmtpClient());
        }

        public EmailService(IEmailViewRenderer emailViewRenderer, IEmailParser emailParser, Func<SmtpClient> createSmtpClient)
        {
            this.emailViewRenderer = emailViewRenderer;
            this.emailParser = emailParser;
            this.createSmtpClient = createSmtpClient;
        }

        private readonly IEmailViewRenderer emailViewRenderer;
        private readonly IEmailParser emailParser;
        private readonly Func<SmtpClient> createSmtpClient;

        public void Send(Email email)
        {
            using (var mailMessage = CreateMailMessage(email))
            using (var smtp = createSmtpClient())
            {
                smtp.Send(mailMessage);
            }
        }

        public Task SendAsync(Email email)
        {
            using (var mailMessage = CreateMailMessage(email))
            using (var smtp = createSmtpClient())
            {
                return smtp.SendMailAsync(mailMessage);
            }
        }

        public void Send(MailMessage mailMessage)
        {
            using (mailMessage)
            using (var smtp = createSmtpClient())
            {
                smtp.Send(mailMessage);
            }
        }

        public Task SendAsync(MailMessage mailMessage)
        {
            using (mailMessage)
            using (var smtp = createSmtpClient())
            {
                return smtp.SendMailAsync(mailMessage);
            }
        }

        public MailMessage CreateMailMessage(Email email)
        {
            var rawEmailString = emailViewRenderer.Render(email);
            var mailMessage = emailParser.Parse(rawEmailString, email);
            return mailMessage;
        }
    }
}