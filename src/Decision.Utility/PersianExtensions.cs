using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Utility
{
    public static class PersianExtensions
    {
        public static string GetPersianNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 48; i < 58; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(1728 + i));
            }
            return data;
        }

        public static string GetEnglishNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 1777; i < 1786; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(i - 1728));
            }
            return data;
        }
        public static string GetPersianNumber(this long data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
        public static string GetPersianNumber(this double data)
        {
            return string.Format(CultureInfo.InvariantCulture,"{0:0.00}", data).GetPersianNumber();
        }
        public static string GetPersianNumber(this int data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this decimal data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
        public static string GetPersianNumber(this byte data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }
    }
}

