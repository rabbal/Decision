using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.AutoMapperProfiles.Extentions
{
   public static class DateTimeExtention
    {
       public static string ToPersianString(this DateTime datetime, PersianDateTimeFormat format = PersianDateTimeFormat.ShortDateShortTime)
       {
           return new PersianDateTime(datetime).ToString(format);
       }

       public static string ToPersianString(this DateTime? datetime, PersianDateTimeFormat format )
       {
           return datetime != null ? new PersianDateTime(datetime.Value).ToString(format) : string.Empty;
       }
    }
}
