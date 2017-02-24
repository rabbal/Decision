using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Decision.DomainClasses.ApplicantInfo;
using Decision.DomainClasses.PrivateMessage;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Users
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        #region Properties
        public long AttachmentsSize { get; set; }

        public long Space { get; set; }

        public bool IsBanned { get; set; }

        public bool IsSystemAccount { get; set; }

        public string LastIp { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool IsChangedPermissions { get; set; }

        public string DirectPermissions { get; set; }

        public XElement XmlDirectPermissions
        {
            get { return XElement.Parse(DirectPermissions); }
            set { DirectPermissions = value.ToString(); }
        }

        public HashSet<string> ConnectionIds { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? LastPasswordChangedDate { get; set; }

        public DateTime? BannedDate { get; set; }

        public string BannedReason { get; set; }

        public DateTime? LastActivityOn { get; set; }

        public string DisplayName { get; set; }

        public bool IsDeleted { get; set; }

        public string AdminComment { get; set; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime RegisterDate { get; set; }

        public string CurrentPageUrl { get; set; }

        public byte[] RowVersion { get; set; }
        #endregion

        #region NavigationProperties
        public ICollection<ActivityLog> Activities { get; set; }

        public ICollection<Applicant> ApprovedApplicants { get; set; }

        public ICollection<Conversation> SentConversations { get; set; }

        public ICollection<Conversation> ReceivedConversations { get; set; }

        public ICollection<Message> SentMessages { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        #endregion
    }
}
