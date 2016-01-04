using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ReferentialTeacher
{
    /// <summary>
    /// ویومدل ویرایش ارجاع استاد
    /// </summary>
    public class EditReferentialTeacherViewModel : BaseIsDelete
    {
        #region Properties
        /// <summary>
        /// آی دی کلاس ارجاع استاد
        /// </summary>
        public  Guid Id { get; set; }

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
        [Required(ErrorMessage = "لطفا کاربر مبدا ارجاع را نتخاب کنید")]
        [DisplayName("کاربر مبدا ارجاع")]
        public  Guid ReferencedFromId { get; set; }

        /// <summary>
        /// آی  دی کاربری که این استاد برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        [Required(ErrorMessage = "لطفا کاربر مقصد ارجاع را نتخاب کنید")]
        [DisplayName("کاربر مقصد ارجاع")]
        public  Guid ReferencedToId { get; set; }

        /// <summary>
        ///  آی دی استاد ارجاع داده شده
        /// </summary>
        [Required(ErrorMessage = "لطفا استاد را نتخاب کنید")]
        [DisplayName("استاد")]
        public  Guid TeacherId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست کاربران برای انتخاب ارجاع دادن استاد به آنها، در لیست آبشاری
        /// </summary>
        public  IEnumerable<SelectListItem> RefrencedToUsers { get; set; }
        #endregion 
    }
}