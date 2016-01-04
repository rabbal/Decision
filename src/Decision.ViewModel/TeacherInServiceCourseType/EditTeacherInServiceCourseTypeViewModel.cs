using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TeacherInServiceCourseType
{
    /// <summary>
    /// ویومدل ویرایش تعداد ساعت یک نوع ضمن خدمت برای استاد
    /// </summary>
    public class EditTeacherInServiceCourseTypeViewModel : BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی تعداد ساعت نوع ضمن خدمت برای استاد
        /// </summary>
        public  Guid Id { get; set; }

        /// <summary>
        /// ساعات گذرانده 
        /// </summary>
        [DisplayName("ساعات گذرانده")]
        [Required(ErrorMessage = "لطفا ساعات گذرانده را مشخص کنید")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا ساعات گذرانده را به شکل صحیح وارد کنید")]
        public  decimal HoursCount { get; set; }

        /// <summary>
        /// آی دی استاد مربوطه
        /// </summary>
        [DisplayName("استاد")]
        [Required]
        public  Guid TeacherId { get; set; }

        /// <summary>
        /// آی دی عنوان دوره ضمن خدمت
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان دوره ی ضمن خدمت را انتخاب کنید")]
        [DisplayName("عنوان دوره ضمن خدمت")]
        public  Guid InServiceCourseTypeTitleId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست عنوان دوره های ضمن خدمت برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> InServiceCourseTypeTitles { get; set; }
        #endregion
    }
}