using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// ویومدل نمایش سوابق تحصیلی متقاضی
    /// </summary>
    public class EducationalBackgroundViewModel : BaseViewModel
    {
        #region Properties
        public Guid ApplicantId { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// مقطع تحصیلی دانشگاهی
        /// </summary>
        [DisplayName("مقطع تحصیلات دانشگاهی")]
        public AcademicDegrees AcademicDegree { get; set; }
        /// <summary>
        /// موضوع پایان نامه مدرک
        /// </summary>
        [DisplayName("موضوع پایان نامه")]
        public string ThesisTopic { get; set; }
        /// <summary>
        /// زمان فارغ التحصیل شدن در این مدرک
        /// </summary>
        [DisplayName("فارغ التحصیل")]
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// تاریخ ورود به مقطع تحصیلی
        /// </summary>
        [DisplayName("شروع")]
        public DateTime EntryDate { get; set; }
        /// <summary>
        /// متقاضی مشاور
        /// </summary>
        [DisplayName("متقاضی مشاور")]
        public string Advisor { get; set; }
        /// <summary>
        /// متقاضی راهنما
        /// </summary>
        [DisplayName("متقاضی راهنما")]
        public string Supervisor { get; set; }
        /// <summary>
        ///توضیحاتی از قبیل نام سایر اساتید مشاور و داوران
        /// </summary>
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        /// <summary>
        /// معدل کل مقطع
        /// </summary>
        [DisplayName("معدل کل")]
        public decimal GPA { get; set; }
        /// <summary>
        /// نمره پایان نامه
        /// </summary>
        [DisplayName("نمره پایان نامه")]
        public decimal ThesisScore { get; set; }
        /// <summary>
        /// امتیاز 
        /// </summary>
        [DisplayName("امتیاز")]
        public double Score { get; set; }
        /// <summary>
        /// کشور محل تحصیل
        /// </summary>
        [DisplayName("کشور")]
        public string Country { get; set; }
        /// <summary>
        /// دانشگاه
        /// </summary>
        [DisplayName("دانشگاه")]
        public string University { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        [DisplayName("رشته دانشگاهی")]
        public string Field { get; set; }
        #endregion
    }
}