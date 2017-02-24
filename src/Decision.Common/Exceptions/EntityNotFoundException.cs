using System;
using System.Runtime.Serialization;

namespace NTierMvcFramework.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string description)
            : base(description)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
        }

        public EntityNotFoundException(string description, Exception inner)
            : base(description, inner)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (inner == null) throw new ArgumentNullException(nameof(inner));
        }

        public EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}