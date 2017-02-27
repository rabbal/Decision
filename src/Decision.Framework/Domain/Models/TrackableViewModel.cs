using System;
using System.ComponentModel;

namespace Decision.Framework.Domain.Models
{
    public abstract class TrackableViewModel
    {
        [DisplayName("تاریخ درج")]
        public DateTime CreationDateTime { get; set; }
        [DisplayName("ایجاد کننده")]
        public string CreatorUserName { get; set; }
        [DisplayName("آخرین ویرایش کننده")]
        public string LastModifierUserName { get; set; }
        [DisplayName("تاریخ آخرین ویرایش")]
        public DateTime LastModificationDateTime { get; set; }
        [DisplayName("آدرس آی پی ایجاد کننده")]
        public string CreatorIp { get; set; }
        [DisplayName("آدرس آی پی آخرین ویرایش کننده")]
        public string LastModifierIp { get; set; }

    }
}