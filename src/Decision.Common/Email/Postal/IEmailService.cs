using System.Net.Mail;
using System.Threading.Tasks;

namespace Decision.Common.Email.Postal
{
    /// <summary>
    /// Creates and send email.
    /// </summary>
    public interface IEmailService
    {
        void Send(Email email);
        Task SendAsync(Email email);
        MailMessage CreateMailMessage(Email email);
        Task SendAsync(MailMessage mailMessage);
        void Send(MailMessage mailMessage);
    }
}
