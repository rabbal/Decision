using System;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// Represents the Notification Record
    /// </summary>
    public class Notification
    {
        #region Ctor
        /// <summary>
        /// create one instance of <see cref="Notification"/>
        /// </summary>
        public Notification()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            ReceivedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets or sets identifier
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// indicate that this notification is read by owner
        /// </summary>
        public  bool IsRead { get; set; }
        /// <summary>
        /// gets or sets notification's text body
        /// </summary>
        public  string Message { get; set; }
        /// <summary>
        /// gets or sets page url that this notification is related with it
        /// </summary>
        public  string Url { get; set; }
        /// <summary>
        /// gets or sets date that this Notification Received
        /// </summary>
        public  DateTime ReceivedOn { get; set; }
        /// <summary>
        /// gets or sets the type of notification
        /// </summary>
        public  NotificationType Type { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// gets or sets the id of user that is owner of this notification
        /// </summary>
        public  long OwnerId { get; set; }
        /// <summary>
        /// gets or sets the user that is owner of this notification
        /// </summary>
        public  User Owner { get; set; }
        #endregion
    }
}
