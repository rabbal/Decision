using System;
using Decision.Framework.Domain;

namespace Decision.DomainClasses.Identity
{
    public class ActivityLog : Entity<Guid>
    {
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTimeOffset LogDateTime { get; set; }

        #endregion

        #region NavigationProperties

        public User User { get; set; }
        public long UserId { get; set; }

        #endregion
    }
}