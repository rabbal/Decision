using System;

namespace Decision.DomainClasses.Common
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        #region Properties
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        #endregion
    }
}