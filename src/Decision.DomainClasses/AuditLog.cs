using System;
using Decision.Framework.Domain;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses
{
    public class AuditLog : Entity
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