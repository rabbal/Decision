using System;

namespace Decision.DomainClasses.Common
{
    public interface IEntity
    {
        #region Properties
        long Id { get; set; }
        Guid RowId { get; set; }
        byte[] RowVersion { get; set; }
        #endregion
    }
}