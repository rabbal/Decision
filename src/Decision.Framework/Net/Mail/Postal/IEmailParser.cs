using System.Net.Mail;

namespace Decision.Framework.Net.Mail.Postal
{
    /// <summary>
    /// Parses raw string output of email views into <see cref="MailMessage"/>.
    /// </summary>
    public interface IEmailParser
    {
        MailMessage Parse(string emailViewOutput, Email email);
    }
}
