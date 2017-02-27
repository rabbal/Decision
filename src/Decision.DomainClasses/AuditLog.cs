using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses
{
    public class AuditLog : CreationTrackingEntity<long, User>
    {
        #region Properties

        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string JsonOriginalValues { get; set; }
        public string JsonNewValues { get; set; }
        public AuditAction Action { get; set; }
        #endregion
    }
}