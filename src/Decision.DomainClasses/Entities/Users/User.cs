using System;
using System.Collections.Generic;

using System.Xml.Linq;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.DomainClasses.Entities.PrivateMessage;
using Decision.Utility;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// نشان دهنده کاربر 
    /// </summary>
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {

        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public User()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            EmailConfirmed = true;
            RegisterDate = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets or sets Total Size of user's Attachments
        /// </summary>
        public long AttachmentsSize { get; set; }
        /// <summary>
        /// gets or sets Total Space That this user can Upload File/image.
        /// this space total retrive from user's role when user login
        /// </summary>
        public long Space { get; set; }
        /// <summary>
        /// نشانده دهنده قفل بودن کاربر است
        /// 
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// آیا کاربر سیستمی است؟
        /// </summary>
        public bool IsSystemAccount { get; set; }
        /// <summary>
        /// آخرین آی پی که ثبت شده برای کاربر
        /// </summary>
        public string LastIp { get; set; }
        /// <summary>
        /// تاریخ آخرین ورود کاربر
        /// </summary>
        public DateTime? LastLoginDate { get; set; }
        /// <summary>
        /// نشان دهنده این است که آیا دسترسی های کاربر تغییر کرده است ؟
        /// </summary>
        public bool IsChangedPermissions { get; set; }
        /// <summary>
        /// دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        public string DirectPermissions { get; set; }
        /// <summary>
        ///  ساختار اکس ام ال دسترسی های مستقیم کاربر بدون وابستی به گروه های کاربری او
        /// </summary>
        public XElement XmlDirectPermissions
        {
            get { return XElement.Parse(DirectPermissions); }
            set { DirectPermissions = value.ToString(); }
        }
        /// <summary>
        /// لیست آی دی هایی که کاربر با آن به سیستم متصل است  
        /// <remarks>منظور همان آی دی های تولیدی هنگام گشودن یک تب در مرورگر در صورت استفاده از 
        /// signalr
        /// </remarks>
        /// </summary>
        public HashSet<string> ConnectionIds { get; set; }
        /// <summary>
        /// indicate this user is Approved Or not
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// gets or sets the last Date that password was changed
        /// </summary>
        public DateTime? LastPasswordChangedDate { get; set; }
        /// <summary>
        /// gets or sets date that this user was banned
        /// </summary>
        public DateTime? BannedDate { get; set; }
        /// <summary>
        /// gets or sets the reason of ban
        /// </summary>
        public string BannedReason { get; set; }
        /// <summary>
        /// gets or sets That Date of User's Last Activity
        /// </summary>
        public DateTime? LastActivityOn { get; set; }
        /// <summary>
        /// gets or sets Name Of User For Show in System
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Indicate That User is Soft Deleted
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// gets or sets one Comment from  Administrator to User
        /// </summary>
        public string AdminComment { get; set; }
        /// <summary>
        /// gets or sets name of avatar's file
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// gets or sets BirthDay
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// gets or sets date that this user registerd
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// gets or sets the page url that use is there now , used for indicate where is user
        /// </summary>
        public string CurrentPageUrl { get; set; }
        /// <summary>
        /// برای مسائل مربوط به همزمانی ها
        /// </summary>
        public byte[] RowVersion { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست فعالیت های کاربر
        /// </summary>
        public ICollection<ActivityLog> Activities { get; set; }
        /// <summary>
        /// لیست اساتیدی که این کاربر به عنوان مدیر نگارش آنها را تایید کرده است
        /// </summary>
        public ICollection<Applicant> ApprovedApplicants { get; set; }
        /// <summary>
        /// لیست گفتگوهای ارسالی کاربر
        /// </summary>
        public ICollection<Conversation> SentConversations { get; set; }
        /// <summary>
        /// لیست گفتگوهای دریافتی کاربر
        /// </summary>
        public ICollection<Conversation> ReceivedConversations { get; set; }
        /// <summary>
        /// لیست پیغام های ارسال شده توسط کاربر
        /// </summary>
        public ICollection<Message> SentMessages { get; set; }
        /// <summary>
        /// لیست آگاه سازی های مربوط به کاربر
        /// </summary>
        public ICollection<Notification> Notifications { get; set; }

        #endregion

    }
}
