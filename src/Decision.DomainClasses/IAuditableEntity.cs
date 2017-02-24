using System;

namespace Decision.DomainClasses.Common
{
    public interface IAuditableEntity : IEntity
    {
        #region Properties
        DateTime CreatedOn { get; set; }
        DateTime LastModifiedOn { get; set; }
        string CreatorIp { get; set; }
        string LastModifierIp { get; set; }
        string LastModifiedBy { get; set; }
        string CreatedBy { get; set; }
        #endregion
    }
}