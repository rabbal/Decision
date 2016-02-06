using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ApplicantInServiceCourseType
{
    /// <summary>
    /// ویومدل نمایش تعداد ساعت یک نوع ضمن خدمت برای متقاضی
    /// </summary>
    public class ApplicantInServiceCourseTypeViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی تعداد ساعت نوع ضمن خدمت برای متقاضی
        /// </summary>
        public  Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// ساعات گذرانده 
        /// </summary>
        [DisplayName("ساعات گذرانده")]
        public  decimal HoursCount { get; set; }

        /// <summary>
        /// نام متقاضی مربوطه
        /// </summary>
        [DisplayName("متقاضی")]
        public  string ApplicantName { get; set; }

        /// <summary>
        /// عنوان دوره ضمن خدمت
        /// </summary>
        [DisplayName("عنوان دوره ضمن خدمت")]
        public  string InServiceCourseTypeTitleName { get; set; }
        #endregion

    }
}