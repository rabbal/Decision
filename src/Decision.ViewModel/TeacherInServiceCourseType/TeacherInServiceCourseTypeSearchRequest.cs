using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TeacherInServiceCourseType
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی تعداد ساعت انواع ضمن خدمت برای استاد
    /// </summary>
    public class TeacherInServiceCourseTypeSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی استاد مربوطه
        /// </summary>
        [DisplayName("استاد")]
        public Guid TeacherId { get; set; }
    }
}