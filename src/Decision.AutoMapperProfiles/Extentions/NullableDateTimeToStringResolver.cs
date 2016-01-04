using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Decision.AutoMapperProfiles.Extentions
{
    public class NullableDateTimeToStringResolver : ValueResolver<DateTime?, string>
    {
        private readonly PersianDateTimeFormat _format;

        public NullableDateTimeToStringResolver(PersianDateTimeFormat format)
        {
            _format = format;
        }
        protected override string ResolveCore(DateTime? source)
        {
            if (source == null) return string.Empty;
            var persianDateTime = new PersianDateTime(source.Value);
            return persianDateTime.ToString(_format);
        }
    }
}
