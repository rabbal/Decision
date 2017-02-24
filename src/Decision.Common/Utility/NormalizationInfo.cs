using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NTierMvcFramework.Common.Utility
{
    public static class NormalizationInfo
    {
        #region Fields
        private static readonly List<string> _allowedEmailDomains =
           new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };
        #endregion
        
        #region Email
        public static bool IsAllowdEmailDomain(this string email)
        {
            var emailDomain = email.FixGmailDots().Split('@')[1];
            return _allowedEmailDomains.Contains(emailDomain);
        }

        public static string FixGmailDots(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            email = email.ToLowerInvariant().Trim();
            var emailParts = email.Split('@');
            var name = emailParts[0].Replace(".", string.Empty).Replace("+", string.Empty);
            var emailDomain = emailParts[1];

            string[] domainsAllowedDots =
            {
                "gmail.com",
                "facebook.com"
            };

            var isFromDomainsAllowedDots = domainsAllowedDots.Any(emailDomain.Equals);
            return !isFromDomainsAllowedDots ? email.ToLowerInvariant() : $"{name}@{emailDomain}".ToLowerInvariant();
        }

        #endregion

        #region RemovePunctuation

        /// <summary>
        ///     reomve ! symboles
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemovePunctuation(this string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? string.Empty
                : new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        #endregion

        #region CleanUnderLines

        public static string CleanUnderLines(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            const char chr1600 = (char)1600; //ـ=1600
            const char chr8204 = (char)8204; //‌=8204

            return text.Replace(chr1600.ToString(CultureInfo.InvariantCulture), "")
                .Replace(chr8204.ToString(CultureInfo.InvariantCulture), "");
        }

        #endregion

        #region RemoveDiacritics

        /// <summary>
        ///     حذف اعراب
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD).Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        #endregion

        #region NormalizeDisplayName,TrimmedNormalizeDisplayName

        public static string NormalizeDisplayName(this string name)
        {
            var result = name.ApplyCorrectYeKe().RemoveDiacritics().CleanUnderLines().RemovePunctuation();
            return result;
        }

        public static string TrimmedNormalizeDisplayName(this string name)
        {
            return name.NormalizeDisplayName().Trim().Replace(" ", "");
        }
        #endregion
    }
}