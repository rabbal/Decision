using System;

namespace Decision.Utility
{
    public static class DateTimeExtention
    {
        public static string ToPersianString(this DateTime datetime, PersianDateTimeFormat format = PersianDateTimeFormat.ShortDateShortTime)
        {
            return new PersianDateTime(datetime).ToString(format).GetPersianNumber();
        }
        public static string ToPersianTimeString(this DateTime datetime)
        {
            return new PersianDateTime(datetime).ToString("hh:mm:ss tt").GetPersianNumber();
        }
        public static string ToPersianString(this DateTime? datetime, PersianDateTimeFormat format)
        {
            return datetime != null ? new PersianDateTime(datetime.Value).ToString(format).GetPersianNumber() : string.Empty;
        }
        public static string ToPersianTimeString(this DateTime? datetime)
        {
            return datetime != null ? new PersianDateTime(datetime.Value).ToString("hh:mm:ss tt").GetPersianNumber() : string.Empty;
        }
        public static string ToRemainingDateTime(this DateTime dateTime)
        {
            return RemainingDateTime.Calculate(dateTime).GetPersianNumber();
        }

    }
}
