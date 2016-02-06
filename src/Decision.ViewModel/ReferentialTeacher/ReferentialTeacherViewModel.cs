using System;
using System.ComponentModel;

namespace Decision.ViewModel.ReferentialApplicant
{
    /// <summary>
    /// ویومدل نمایش ارجاعات متقاضی
    /// </summary>
    public class ReferentialApplicantViewModel
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
        /// آی  دی کاربری که این متقاضی را ارجاع داده است
        /// </summary>
        [DisplayName("کاربر مبدا ارجاع")]
        public  string ReferencedFromName { get; set; }

        /// <summary>
        /// آی  دی کاربری که این متقاضی برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        [DisplayName("کاربر مقصد ارجاع")]
        public  string ReferencedToName { get; set; }

        /// <summary>
        ///  آی دی متقاضی ارجاع داده شده
        /// </summary>
        [DisplayName("متقاضی")]
        public  string ApplicantName { get; set; }
        #endregion
    }
}