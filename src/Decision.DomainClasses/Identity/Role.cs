using System;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class Role : IdentityRole<long, UserRole>, ITrackable<Role>, ISystemDefaultEntry, IEntity<long>
    {
        #region Properties

        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsSystemEntry { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public string LastModifierBrowserName { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? CreatorUserId { get; set; }
        public byte[] RowVersion { get; set; }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Navigation Properties

        public Role CreatorUser { get; set; }
        public Role LastModifierUser { get; set; }

        #endregion
    }
}