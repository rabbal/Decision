using System;
using System.ComponentModel;

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
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// آخرین تاریخ ویرایش
        /// </summary>
        [DisplayName("تاریخ آخرین ویرایش")]
        public DateTime? LastModifiedDate { get; set; }
    }
}