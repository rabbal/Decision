using System;
using Decision.Common.Domain;
using Decision.Common.Domain.Tracking;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses
{
    public class Attachment : Entity<Guid>, ICreationTracking<User>, IHasModificationDateTime
    {
        #region Properties

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
        public long DownloadsCount { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public string CreatorIp { get; set; }
        public string CreatorBrowserName { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }

        #endregion

        #region NavigationProperties

        public long? CreatorUserId { get; set; }
        public User CreatorUser { get; set; }
        #endregion

    }
}