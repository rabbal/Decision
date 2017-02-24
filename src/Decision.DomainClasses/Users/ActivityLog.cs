using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Users
{
    public class ActivityLog : IHaveGuidKey
    {
        #region Properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string OperantIp { get; set; }
        public DateTime OperatedOn { get; set; }
        #endregion

        #region NavigationProperties
        public User User { get; set; }
        public long UserId { get; set; }
        #endregion
    }
}
