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
        public virtual Guid Id { get; set; }
        /// <summary>
        /// sets or gets name for attachment
        /// </summary>
        public virtual string FileName { get; set; }
        /// <summary>
        /// sets or gets type of attachment
        /// </summary>
        public virtual string ContentType { get; set; }
        /// <summary>
        /// sets or gets size of attachment
        /// </summary>
        public virtual long Size { get; set; }
        /// <summary>
        /// sets or gets Extention of attachment
        /// </summary>
        public virtual string Extension { get; set; }
        /// <summary>
        /// sets or gets bytes of data
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// sets or gets Creation Date
        /// </summary>
        public virtual DateTime AttachedOn { get; set; }
        /// <summary>
        /// gets or sets counts of download this file
        /// </summary>
        public virtual long DownloadsCount { get; set; }
        /// <summary>
        /// gets or sets datetime that is modified
        /// </summary>
        public virtual DateTime ModifiedOn { get; set; }
        /// <summary>
        /// gets or sets information of user agent 
        /// </summary>
        public virtual string Agent { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// sets or gets identifier of attachment's owner
        /// </summary>
        public virtual long OwnerId { get; set; }
        /// <summary>
        /// sets or gets identifier of attachment's owner
        /// </summary>
        public virtual User Owner { get; set; }
        #endregion
    }
}
