using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده سابقه تحصیلی متقاضی
    /// </summary>
    public class EducationalBackground : BaseEntity
    {
        #region Properties
        /// <summary>
        /// امتیاز 
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// مقطع تحصیلی دانشگاهی
        /// </summary>
        public AcademicDegrees AcademicDegree { get; set; }
        /// <summary>
        /// موضوع پایان نامه مدرک
        /// </summary>
        public string ThesisTopic { get; set; }
        /// <summary>
        /// زمان فارغ التحصیل شدن در این مدرک
        /// </summary>
        public DateTime GraduationDate { get; set; }
        /// <summary>
        /// تاریخ ورود به مقطع تحصیلی
        /// </summary>
        public DateTime EntryDate { get; set; }
        /// <summary>
        /// استاد مشاور
        /// </summary>
        public string Advisor { get; set; }
        /// <summary>
        /// استاد راهنما
        /// </summary>
        public string Supervisor { get; set; }
        /// <summary>
        ///توضیحاتی از قبیل نام سایر اساتید مشاور و داوران
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// معدل کل مقطع
        /// </summary>
        public decimal GPA { get; set; }
        /// <summary>
        /// نمره پایان نامه
        /// </summary>
        public decimal ThesisScore { get; set; }
        /// <summary>
        /// کشور محل تحصیل
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// دانشگاه
        /// </summary>
        public string University { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string Field { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی متقاضی صاحب مدرک
        /// </summary>
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// متقاضی صاحب مدرک
        /// </summary>
        public Applicant Applicant { get; set; }
        #endregion
    }
}
