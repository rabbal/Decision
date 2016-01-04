using System.Text.RegularExpressions;

namespace Decision.Common.RSS
{
    public static class RtlHelper
    {
        private static readonly Regex MatchArabicHebrew =
            new Regex(@"[\u0600-\u06FF,\u0590-\u05FF]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex MatchHexadecimalSymbols =
            new Regex("[\x00-\x08\x0B\x0C\x0E-\x1F]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string CorrectRtl(this string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return string.Empty;

            const char rleChar = (char)0x202B;
            if (MatchArabicHebrew.IsMatch(title))
                return rleChar + title;
            return title;
        }

        public static string CorrectRtlBody(this string body)
        {
            if (string.IsNullOrWhiteSpace(body)) return string.Empty;

            if (MatchArabicHebrew.IsMatch(body))
                return "<div style='text-align: right; font-family:tahoma; font-size:9pt;' dir='rtl'>" + body + "</div>";
            return "<div style='text-align: left; font-family:tahoma; font-size:9pt;' dir='ltr'>" + body + "</div>";
        }

        /// <summary>
        /// there are a lot of symbols which can't be in xml code.
        /// </summary>
        public static string RemoveHexadecimalSymbols(this string txt)
        {
            return string.IsNullOrWhiteSpace(txt) ?
                string.Empty : MatchHexadecimalSymbols.Replace(txt, string.Empty);
        }
    }
}