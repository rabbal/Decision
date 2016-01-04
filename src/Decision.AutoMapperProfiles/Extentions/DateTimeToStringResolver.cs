using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Decision.AutoMapperProfiles.Extentions
{
    public class DateTimeToStringResolver : ValueResolver<DateTime,string>
    {
        private readonly PersianDateTimeFormat _format;

        public DateTimeToStringResolver(PersianDateTimeFormat format)
        {
            _format = format;
        }
        protected override string ResolveCore(DateTime source)
        {
            var persianDateTime=new PersianDateTime(source);
            return persianDateTime.ToString(_format);
        }
    }
}
