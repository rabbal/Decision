using System;
using System.ComponentModel;
using Decision.DomainClasses;

namespace Decision.ViewModels.Common
{
    public abstract class BaseViewModel
    {
        [DisplayName("ایجاد کننده")]
        public string CreatorUserName { get; set; }
        [DisplayName("آخرین ویرایش کننده")]
        public string LastModifierUserName { get; set; }
        [DisplayName("حذف شده")]
        public bool IsDeleted { get; set; }
        [DisplayName("تاریخ درج")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("تاریخ آخرین ویرایش")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("نوع عملیات")]
        public AuditAction Action { get; set; }
        //public  string CreatorIp { get; set; }
        //public  string ModifierIp { get; set; }
        //public  bool ModifyLocked { get; set; }
        //public  string ModifierAgent { get; set; }
        //public  string CreatorAgent { get; set; }
        public int Version { get; set; }
    }
}