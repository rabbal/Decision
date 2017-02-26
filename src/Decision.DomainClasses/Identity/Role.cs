using System;
using System.Collections.Generic;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class Role : IdentityRole<long, UserRole>, ITrackable<Role>, ISystemDefaultEntry, IEntity<long>
    {
        #region Constructor

        public Role()
        {
            Claims = new HashSet<RoleClaim>();
        }
        #endregion

        #region Properties

        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsSystemEntry { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LasModificationDateTime { get; set; }
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
        public ICollection<RoleClaim> Claims { get; set; }

        #endregion
    }
}