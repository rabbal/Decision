using System;
using Decision.Common.Domain;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses
{
    public class AuditLog : Entity<Guid>
    {
        #region Properties

        public string EntityId { get; set; }
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