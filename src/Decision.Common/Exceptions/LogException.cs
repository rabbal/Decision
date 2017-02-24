using System;
using System.Runtime.Serialization;

namespace NTierMvcFramework.Common.Exceptions
{
    public class LogException : Exception
    {
        public LogException(string description)
            : base(description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
        }

        public LogException(string description, Exception inner)
            : base(description, inner)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
        }

        public LogException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
