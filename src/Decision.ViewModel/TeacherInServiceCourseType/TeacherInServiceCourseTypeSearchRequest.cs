using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ApplicantInServiceCourseType
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی تعداد ساعت انواع ضمن خدمت برای متقاضی
    /// </summary>
    public class ApplicantInServiceCourseTypeSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوطه
        /// </summary>
        [DisplayName("متقاضی")]
        public Guid ApplicantId { get; set; }
    }
}