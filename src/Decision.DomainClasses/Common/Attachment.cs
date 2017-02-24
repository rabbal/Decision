using System;
using Decision.DomainClasses.Users;

namespace Decision.DomainClasses.Common
{
    public class Attachment
    {
        #region Constructors
        protected Attachment()
        {
            AttachedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public DateTime AttachedOn { get; set; }
        public long DownloadsCount { get; set; }
        public DateTime ModifiedOn { get; set; }
        #endregion

        #region NavigationProperties
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        #endregion
    }
}
