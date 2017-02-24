using System;
using Decision.DomainClasses.Users;

namespace Decision.DomainClasses.Common
{
    public enum AuditAction
    {
        Create,
        Modify,
        SoftDelete,
    }

    public class AuditLog : IHaveGuidKey
    {
        #region Properties
        public Guid Id { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public string JsonOriginalValues { get; set; }
        public string JsonNewValues { get; set; }
        public AuditAction Action { get; set; }
        #endregion

        #region NavigationProperties
        public User User { get; set; }
        public long UserId { get; set; }
        #endregion
    }
}
