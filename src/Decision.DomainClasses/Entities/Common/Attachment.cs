using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// Represents a base class for every attachment
    /// </summary>
    public class Attachment
    {
        #region Ctor
        /// <summary>
        /// create one instance of <see cref="Attachment"/>
        /// </summary>
        protected Attachment()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            AttachedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// sets or gets identifier for attachment
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// sets or gets name for attachment
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// sets or gets type of attachment
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// sets or gets size of attachment
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// sets or gets Extention of attachment
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// sets or gets bytes of data
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// sets or gets Creation Date
        /// </summary>
        public DateTime AttachedOn { get; set; }
        /// <summary>
        /// gets or sets counts of download this file
        /// </summary>
        public long DownloadsCount { get; set; }
        /// <summary>
        /// gets or sets datetime that is modified
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// gets or sets information of user agent 
        /// </summary>
        public string Agent { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// sets or gets identifier of attachment's owner
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// sets or gets identifier of attachment's owner
        /// </summary>
        public User Owner { get; set; }
        #endregion
    }
}
