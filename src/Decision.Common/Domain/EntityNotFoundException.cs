using System;
using System.Runtime.Serialization;

namespace Decision.Common.Domain
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        #region Properties
        public Type EntityType { get; set; }
        public object Id { get; set; }

        #endregion

        #region Constructors
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public EntityNotFoundException(Type entityType, object id, Exception innerException = null)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        #endregion
    }
}