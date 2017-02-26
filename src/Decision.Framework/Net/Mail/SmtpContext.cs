using System.Net;
using System.Net.Mail;
using Decision.Framework.GuardToolkit;

namespace Decision.Framework.Net.Mail
{
    public class SmtpContext
    {
        public SmtpContext(string host, int port = 25)
        {
            Check.ArgumentNotEmpty(() => host);
            Check.ArgumentIsPositive(port, nameof(port));

            Host = host;
            Port = port;
        }

        public SmtpContext(EmailAccount account)
        {
            Check.ArgumentNotNull(() => account);

            Host = account.Host;
            Port = account.Port;
            EnableSsl = account.EnableSsl;
            Password = account.Password;
            UseDefaultCredentials = account.UseDefaultCredentials;
            Username = account.Username;
        }

        public SmtpContext()
        {
        }

        public bool UseDefaultCredentials { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }

        public SmtpClient ToSmtpClient()
        {
            using (var smtpClient = new SmtpClient(Host, Port))
            {
                smtpClient.UseDefaultCredentials = UseDefaultCredentials;
                smtpClient.EnableSsl = EnableSsl;

                smtpClient.Credentials = UseDefaultCredentials
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(Username, Password);

                return smtpClient;
            }
        }
    }
}