using System;
using Decision.Framework.Domain;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Identity
{
    public class Notification : Entity, IHasRowLevelSecurity
    {
        #region Constructor

        public Notification()
        {
            ReceivedDateTime = DateTimeOffset.UtcNow;
        }

        #endregion

        #region Properties

        public bool IsRead { get; set; }
        public bool IsDismissed { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTimeOffset ReceivedDateTime { get; set; }
        public NotificationType Type { get; set; }

        #endregion

        #region NavigationProperties

        public long UserId { get; set; }
        public User User { get; set; }

        #endregion
    }
}