using System;

namespace Decision.Utility
{
    public static class CalculateReadingTime
    {
        public static string MinReadTime(this string text, int wordsPerMinute = 180)
        {
            var wordsCount = text.WordsCount();
            var minutes = wordsCount / wordsPerMinute;
            return minutes == 0 ? "کمتر از یک دقیقه" : TimeSpan.FromMinutes(minutes).ToReadableString();
        }

        private static string ToReadableString(this TimeSpan span)
        {
            var formatted =
                $"{(span.Duration().Days > 0 ? $"{span.Days:0} روز و " : string.Empty)}{(span.Duration().Hours > 0 ? $"{span.Hours:0} ساعت و " : string.Empty)}{(span.Duration().Minutes > 0 ? $"{span.Minutes:0} دقیقه و " : string.Empty)}{(span.Duration().Seconds > 0 ? $"{span.Seconds:0} ثانیه" : string.Empty)}";

            if (formatted.EndsWith("و "))
            {
                formatted = formatted.Substring(0, formatted.Length - 2);
            }

            if (string.IsNullOrEmpty(formatted))
            {
                formatted = "0 ثانیه";
            }
            return formatted.Trim();
        }
    }
}
