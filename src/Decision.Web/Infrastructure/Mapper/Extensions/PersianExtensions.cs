using System;
using System.Globalization;

namespace Decision.Web.Infrastructure.Mapper.Extensions
{

    public static class PersianExtensions
    {
        public static string ToShamsiDateTime(this DateTime info)
        {
            var year = info.Year;
            var month = info.Month;
            var day = info.Day;
            var persianCalendar = new PersianCalendar();
            var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
            var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
            var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));
            return string.Format("{0}{1}{2}{1}{3}", pYear, "/", pMonth.ToString("00", CultureInfo.InvariantCulture),
                pDay.ToString("00", CultureInfo.InvariantCulture));
        }
    }
}