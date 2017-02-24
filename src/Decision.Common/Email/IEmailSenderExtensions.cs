//namespace NTierMvcFramework.Core.Email
//{
//    public static class IEmailSenderExtensions
//    {

//        public static bool SendEmail(this IEmailSender sender, string from, string to, string subject)
//        {
//            var emailMessage = new EmailMessage();
//            var toEmailAddress = new EmailAddress(to);
//            emailMessage.To.Add(toEmailAddress);
//            emailMessage.Subject = subject;

//            return sender.SendEmail(new SmtpContext(), emailMessage);
//        }

//        public static bool SendEmail(this IEmailSender sender, string from, string to, string subject, string message)
//        {
//            var emailMessage = new EmailMessage();
//            var toEmailAddress = new EmailAddress(to);
//            emailMessage.To.Add(toEmailAddress);
//            emailMessage.Subject = subject;
//            emailMessage.Body = message;

//            return sender.SendEmail(new SmtpContext(), emailMessage);
//        }

//    }
//}

