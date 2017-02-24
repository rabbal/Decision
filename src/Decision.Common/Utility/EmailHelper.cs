using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Decision.Common.Utility
{
    public static class EmailHelper
    {
        /// <summary>
        /// استخراج ایمیل‌های یک فایل متنی و ذخیره آن در فایلی جدید
        /// </summary>
        /// <param name="inFilePath">فایل ورودی</param>
        /// <param name="outFilePath">فایل خروجی</param>
        public static void ExtractEmails(string inFilePath, string outFilePath)
        {
            var data = File.ReadAllText(inFilePath); 
                                                        
            var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                RegexOptions.IgnoreCase);

            var emailMatches = emailRegex.Matches(data);

            var sb = new StringBuilder();
            foreach (Match emailMatch in emailMatches)
            {
                if(IsValidEmail(emailMatch.Value))
                    sb.AppendLine(emailMatch.Value);
            }

            File.WriteAllText(outFilePath, sb.ToString());
        }

        public static bool IsValidEmail(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            try
            {
                var address = new MailAddress(emailAddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}