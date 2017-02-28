using System;
using System.ComponentModel;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.EducationalBackground
{
    public class EducationalBackgroundViewModel 
    {
        #region Properties
        public Guid ApplicantId { get; set; }
        public Guid Id { get; set; }
        [DisplayName("مقطع تحصیلات دانشگاهی")]
        public AcademicDegrees AcademicDegree { get; set; }
        [DisplayName("موضوع پایان نامه")]
        public string ThesisTopic { get; set; }
        [DisplayName("فارغ التحصیل")]
        public DateTime GraduationDate { get; set; }
        [DisplayName("شروع")]
        public DateTime EntryDate { get; set; }
        [DisplayName("متقاضی مشاور")]
        public string Advisor { get; set; }
        [DisplayName("متقاضی راهنما")]
        public string Supervisor { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("معدل کل")]
        public decimal GPA { get; set; }
        [DisplayName("نمره پایان نامه")]
        public decimal ThesisScore { get; set; }
        [DisplayName("امتیاز")]
        public double Score { get; set; }
        [DisplayName("کشور")]
        public string Country { get; set; }
        [DisplayName("دانشگاه")]
        public string University { get; set; }
        [DisplayName("رشته دانشگاهی")]
        public string Field { get; set; }
        #endregion
    }
}