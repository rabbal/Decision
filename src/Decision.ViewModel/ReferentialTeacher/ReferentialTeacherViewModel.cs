using System;
using System.ComponentModel;

namespace Decision.ViewModel.ReferentialTeacher
{
    /// <summary>
    /// ویومدل نمایش ارجاعات استاد
    /// </summary>
    public class ReferentialTeacherViewModel
    {
        #region Properties
        /// <summary>
        /// تاریخ ایجاد این ارجاع
        /// </summary>
        [DisplayName("تاریخ ارجاع")]
        public  DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخی که این ارجاع تکمیل شده است
        /// <remarks>وقتی کاربر مورد نظر اصلاحات لازم را انجام داد و کلید تکمیل را  فشرد</remarks>
        /// </summary>
        [DisplayName("تاریخ تکمیل ارجاع")]
        public  DateTime? FinishedDate { get; set; }

        /// <summary>
        /// آی  دی کاربری که این استاد را ارجاع داده است
        /// </summary>
        [DisplayName("کاربر مبدا ارجاع")]
        public  string ReferencedFromName { get; set; }

        /// <summary>
        /// آی  دی کاربری که این استاد برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        [DisplayName("کاربر مقصد ارجاع")]
        public  string ReferencedToName { get; set; }

        /// <summary>
        ///  آی دی استاد ارجاع داده شده
        /// </summary>
        [DisplayName("استاد")]
        public  string TeacherName { get; set; }
        #endregion
    }
}