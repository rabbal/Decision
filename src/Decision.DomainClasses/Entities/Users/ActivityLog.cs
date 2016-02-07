using System;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// Represents Activity Log record
    /// </summary>
    public class ActivityLog
    {
        #region Ctor
        /// <summary>
        /// Create one instance of <see cref="ActivityLog"/>
        /// </summary>
        public ActivityLog()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            OperatedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets or sets identifier 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// gets or sets the comment of this activity
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// gets or sets the date that this activity was done
        /// </summary>
        public DateTime OperatedOn { get; set; }
        /// <summary>
        /// gets or sets the page url . 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// gets or sets the title of page if Url is Not null
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// gets or sets user agent information
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// gets or sets user's ip address
        /// </summary>
        public string OperantIp { get; set; }
        /// <summary>
        /// gets or sets the type of this activity
        /// </summary>
        public ActivityLogType Type { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// gets or sets User that done this activity
        /// </summary>
        public User Operant { get; set; }
        /// <summary>
        /// gets or sets Id of User that done this activity
        /// </summary>
        public Guid OperantId { get; set; }
        #endregion
    }
}
