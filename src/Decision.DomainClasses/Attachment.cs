using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses
{
    public class Attachment : CreationTrackingEntity<long, User>, IHasModificationDateTime
    {
        #region Properties
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        public byte[] Content { get; set; }
        public long DownloadsCount { get; set; }
        public DateTime? LasModificationDateTime { get; set; }

        #endregion
    }
}