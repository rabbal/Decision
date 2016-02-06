using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// ویومدل نمایش سوابق تحصیلی متقاضی
    /// </summary>
    public class EducationalBackgroundViewModel :BaseViewModel
    {
        #region Properties
        public Guid ApplicantId { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// نوع تحصیلات 
        /// حوزوی / دانشگاهی
        /// </summary>
        [DisplayName("نوع تحصیلات")]
        public  EducationalType EducationalType { get; set; }

        /// <summary>
        /// مقطع تحصیلات حوزوی
        /// </summary>
        [DisplayName("مقطع تحصیلات حوزی")]
        public  HozeDegrees HosDegree { get; set; }

        /// <summary>
        /// مقطع تحصیلی دانشگاهی
        /// </summary>
        [DisplayName("مقطع تحصیلات دانشگاهی")]
        public  AcademicDegrees AcademicDegree { get; set; }

        /// <summary>
        /// موضوع پایان نامه مدرک
        /// </summary>
        [DisplayName("موضوع پایان نامه")]
        public  string ThesisTopic { get; set; }

        /// <summary>
        /// زمان فارغ التحصیل شدن در این مدرک
        /// </summary>
        [DisplayName("فارغ التحصیل")]
        public  DateTime GraduationDate { get; set; }

        /// <summary>
        /// تاریخ ورود به مقطع تحصیلی
        /// </summary>
        [DisplayName("شروع")]
        public  DateTime EntryDate { get; set; }

        /// <summary>
        /// متقاضی مشاور
        /// </summary>
        [DisplayName("متقاضی مشاور")]
        public  string Advisor { get; set; }

        /// <summary>
        /// متقاضی راهنما
        /// </summary>
        [DisplayName("متقاضی راهنما")]
        public  string Supervisor { get; set; }

        /// <summary>
        ///توضیحاتی از قبیل نام سایر اساتید مشاور و داوران
        /// </summary>
        [DisplayName("توضیحات")]
        public  string Description { get; set; }

        /// <summary>
        /// معدل کل مقطع
        /// </summary>
        [DisplayName("معدل کل")]
        public  decimal GPA { get; set; }

        /// <summary>
        /// نمره پایان نامه
        /// </summary>
        [DisplayName("نمره پایان نامه")]
        public  decimal ThesisScore { get; set; }

        /// <summary>
        /// میزان ارتباط با پست کاری
        /// </summary>
        [DisplayName("ارتباط با پست کاری")]
        public int RelatedToOrganizationPosition { get; set; }
        /// <summary>
        /// آی دی موسسه آموزشی
        /// </summary>
        [DisplayName("موسسه آموزشی")]
        public  string InstitutionName { get; set; }

        /// <summary>
        /// آی دی رشته تحصیلی
        /// </summary>
        [DisplayName("رشته تحصیلی")]
        public  string StudyFieldName { get; set; }

        public byte[] Attachment { get; set; }
        #endregion
    }
}