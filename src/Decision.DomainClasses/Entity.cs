using System;

namespace Decision.DomainClasses.Common
{
    public abstract class Entity : IEntity
    {
        #region Properties
        public long Id { get; set; }
        public Guid RowId { get; set; }
        public byte[] RowVersion { get; set; }
        #endregion
    }
}