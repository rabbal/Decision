using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Decision.Utility;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Entities.Users
{
    /// <summary>
    /// نشان دهنده گروه کاربری
    /// </summary>
    public class Role : IdentityRole<Guid, UserRole>
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Role()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آیا گروه سیستمی هستند؟
        /// </summary>
        public bool IsSystemRole { get; set; }
        /// <summary>
        /// برای مسائل همزمانی 
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// آیا حساب  کاربران این گروه کاربری مسدود شود؟
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// لیست دسترسی های گروه کاربری
        /// </summary>
        public string Permissions { get; set; }
        /// <summary>
        /// فرمت ایکس ام ال دسترسی ها
        /// </summary>
        public XElement XmlPermissions
        {
            get { return XElement.Parse(Permissions); }
            set { Permissions = value.ToString(); }
        }
        #endregion
    }
}
