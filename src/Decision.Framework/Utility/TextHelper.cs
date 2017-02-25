using System;
using System.Text;

namespace Decision.Framework.Utility
{
    public static class TextHelper
    {
        public static string Base64ToString(string charset, string encodedString)
        {
            var buffer = Convert.FromBase64String(encodedString);

            return Encoding.GetEncoding(charset).GetString(buffer);
        }

        public static string StringToBase64(string charset, string orginalString)
        {

            var buffer = System.Text.Encoding.GetEncoding(charset).GetBytes(orginalString);

            return Convert.ToBase64String(buffer);
        }

        public static string ToUtf8Base64(this string orginalString)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(orginalString);
            return Convert.ToBase64String(buffer);
        }
    }
}