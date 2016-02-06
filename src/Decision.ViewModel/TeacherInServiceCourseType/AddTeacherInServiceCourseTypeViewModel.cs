using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModel.ApplicantInServiceCourseType
{
    /// <summary>
    /// ویومدل درج تعداد ساعت یک نوع ضمن خدمت برای متقاضی
    /// </summary>
    public class AddApplicantInServiceCourseTypeViewModel
    {
        #region Properties
        /// <summary>
        /// ساعات گذرانده 
        /// </summary>
        [DisplayName("ساعات گذرانده")]
        [Required(ErrorMessage = "لطفا ساعات گذرانده را مشخص کنید")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا ساعات گذرانده را به شکل صحیح وارد کنید")]
        public  decimal HoursCount { get; set; }

        /// <summary>
        /// آی دی متقاضی مربوطه
        /// </summary>
        [DisplayName("متقاضی")]
        [Required]
        public  Guid ApplicantId { get; set; }

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