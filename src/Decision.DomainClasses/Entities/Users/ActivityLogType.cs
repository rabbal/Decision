using System;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// Represents Activity Log Type Record
    /// </summary>
    public class ActivityLogType
    {
        #region Ctor
        /// <summary>
        /// Create one Instance of <see cref="ActivityLogType"/>
        /// </summary>
        public ActivityLogType()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets or sets identifier 
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// gets or sets the system name
        /// </summary>
        public virtual string Name{ get; set; }
        /// <summary>
        /// gets or sets the display name
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// gets or sets the description 
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// indicate this log type is enable for logging
        /// </summary>
        public virtual bool IsEnabled { get; set; }
        #endregion
    }
}
