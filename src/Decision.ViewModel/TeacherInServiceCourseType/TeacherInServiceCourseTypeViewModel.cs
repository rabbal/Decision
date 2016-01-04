using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TeacherInServiceCourseType
{
    /// <summary>
    /// ویومدل نمایش تعداد ساعت یک نوع ضمن خدمت برای استاد
    /// </summary>
    public class TeacherInServiceCourseTypeViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی تعداد ساعت نوع ضمن خدمت برای استاد
        /// </summary>
        public  Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        /// <summary>
        /// ساعات گذرانده 
        /// </summary>
        [DisplayName("ساعات گذرانده")]
        public  decimal HoursCount { get; set; }

        /// <summary>
        /// نام استاد مربوطه
        /// </summary>
        [DisplayName("استاد")]
        public  string TeacherName { get; set; }

        /// <summary>
        /// عنوان دوره ضمن خدمت
        /// </summary>
        [DisplayName("عنوان دوره ضمن خدمت")]
        public  string InServiceCourseTypeTitleName { get; set; }
        #endregion

    }
}