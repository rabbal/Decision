using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده سابقه تحصیلی استاد
    /// </summary>
    public class EducationalBackground : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public EducationalBackground()
        {
            Attachment = BitConverter.GetBytes(0);
        }
        #endregion

        #region Properties
        /// <summary>
        /// نوع تحصیلات 
        /// حوزوی / دانشگاهی
        /// </summary>
        public  EducationalType EducationalType { get; set; }
        /// <summary>
        /// مقطع تحصیلی دانشگاهی
        /// </summary>
        public  AcademicDegrees AcademicDegree { get; set; }
        /// <summary>
        /// موضوع پایان نامه مدرک
        /// </summary>
        public  string ThesisTopic { get; set; }
        /// <summary>
        /// زمان فارغ التحصیل شدن در این مدرک
        /// </summary>
        public  DateTime GraduationDate { get; set; }
        /// <summary>
        /// تاریخ ورود به مقطع تحصیلی
        /// </summary>
        public  DateTime  EntryDate { get; set; }
        /// <summary>
        /// استاد مشاور
        /// </summary>
        public  string Advisor { get; set; }
        /// <summary>
        /// استاد راهنما
        /// </summary>
        public  string Supervisor { get; set; }
        /// <summary>
        ///توضیحاتی از قبیل نام سایر اساتید مشاور و داوران
        /// </summary>
        public  string Description { get; set; }
        /// <summary>
        /// معدل کل مقطع
        /// </summary>
        public  decimal GPA { get; set; }
        /// <summary>
        /// نمره پایان نامه
        /// </summary>
        public  decimal ThesisScore { get; set; }
        /// <summary>
        /// میزان ارتباط با پست کاری
        /// </summary>
        public  int RelatedToOrganizationPosition { get; set; }
        /// <summary>
        /// فایل ضمیمه مدرک تحصیلی/filestream
        /// </summary>
        public  byte[] Attachment { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی استاد صاحب مدرک
        /// </summary>
        public  Guid TeacherId { get; set; }
      
        /// <summary>
        /// آی دی موسسه آموزشی
        /// </summary>
        public  Guid InstitutionId { get; set; }
        /// <summary>
        /// آی دی رشته تحصیلی
        /// </summary>
        public  Guid StudyFieldId { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public  Title StudyField { get; set; }
        /// <summary>
        /// استاد صاحب مدرک
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// موسسه آموزشی 
        /// </summary>
        public  Institution Institution { get; set; }
        #endregion
    }
}
