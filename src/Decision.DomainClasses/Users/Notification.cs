using System;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Users
{
    public enum NotificationType
    {
        Registration,
    }

    public class Notification : IHasGuidKey
    {
        #region Constructor
        public Notification()
        {
            ReceivedOn = DateTime.Now;
        }
        #endregion

        #region Properties

        public Guid Id { get; set; }
        public bool IsDismissed { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime ReceivedOn { get; set; }
        public NotificationType Type { get; set; }
        #endregion

        #region NavigationProperties
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}