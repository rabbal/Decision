using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses
{
    public class Attachment : Entity, ICreationTracking<User>, IHasModificationDateTime
    {
        #region Properties

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
        public long DownloadsCount { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public DateTime? LasModificationDateTime { get; set; }

        #endregion

        #region NavigationProperties

        public long? CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        #endregion
    }
}