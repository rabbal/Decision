using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Decision.Framework.Domain;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, ITrackable<User>,
        ISystemDefaultEntry, IEntity, IActivatable
    {
     
        #region Public Methods

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        public string NormalizedUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsSystemEntry { get; set; }
        public DateTime? LastVisitDateTime { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }

        public byte[] PhotoContent { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LasModificationDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string LastModifierIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public string LastModifierBrowserName { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? CreatorUserId { get; set; }
        public byte[] RowVersion { get; set; }

        #endregion

        #region Navigation Properties

        public User CreatorUser { get; set; }
        public User LastModifierUser { get; set; }
        #endregion
    }
}