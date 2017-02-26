using System;
using Decision.Framework.Domain;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Identity
{
    public class Notification : Entity<Guid>, IHasRowLevelSecurity
    {
        #region Constructor

        public Notification()
        {
            ReceivedDateTime = DateTime.Now;
        }

        #endregion

        #region Properties

        public bool IsRead { get; set; }
        public bool IsDismissed { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public NotificationType Type { get; set; }

        #endregion

        #region Navigation Properties

        public long UserId { get; set; }
        public User User { get; set; }

        #endregion
    }
}