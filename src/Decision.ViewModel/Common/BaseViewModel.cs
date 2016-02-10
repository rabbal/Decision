using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.Common;

namespace Decision.ViewModel.Common
{
    public abstract class BaseViewModel
    {
        /// <summary>
        /// نام کاربری ایجاد کننده رکورد
        /// </summary>
        [DisplayName("ایجاد کننده")]
        public string CreatorUserName { get; set; }
        /// <summary>
        /// نام کاربری آخرین ویرایش کننده رکورد
        /// </summary>
        [DisplayName("آخرین ویرایش کننده")]
        public string LastModifierUserName { get; set; }
        /// <summary>
        /// حذف شده است؟
        /// </summary>
        [DisplayName("حذف شده")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// تاریخ درج
        /// </summary>
        [DisplayName("تاریخ درج")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// آخرین تاریخ ویرایش
        /// </summary>
        [DisplayName("تاریخ آخرین ویرایش")]
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// نوع اکشن انجام شده
        /// </summary>
        [DisplayName("نوع عملیات")]
        public AuditAction Action { get; set; }
        ///// <summary>
        ///// gets or sets IP Address of Creator
        ///// </summary>
        //public  string CreatorIp { get; set; }
        ///// <summary>
        ///// gets or set IP Address of Modifier
        ///// </summary>
        //public  string ModifierIp { get; set; }
        ///// <summary>
        ///// indicate this entity is Locked for Modify
        ///// </summary>
        //public  bool ModifyLocked { get; set; }
        ///// <summary>
        ///// gets or sets information of user agent of modifier
        ///// </summary>
        //public  string ModifierAgent { get; set; }
        ///// <summary>
        ///// gets or sets information of user agent of Creator
        ///// </summary>
        //public  string CreatorAgent { get; set; }
        /// <summary>
        /// gets or sets count of Modification Default is 1
        /// </summary>
        public int Version { get; set; }
    }
}