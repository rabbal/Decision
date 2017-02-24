using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Users
{
    public class Role : IdentityRole<long, UserRole>, IAuditableEntity
    { 
        #region Properties
        public bool IsSystemRole { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public Guid RowId { get; set; }
        public byte[] RowVersion { get; set; }
        #endregion
    }
}
