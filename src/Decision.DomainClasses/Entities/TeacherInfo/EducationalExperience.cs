using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// مشخص کننده سوابق آموزشی استاد
    /// <remarks>تدریس در مراکز عملی/تدریس در مراکز سازمانی/اولویت های آموزشی تصویب شده برای استاد/موضوعات مورد علاقه استاد برای تدریس</remarks>
    /// </summary>
    public class EducationalExperience : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public EducationalExperience()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// فیلد مشترک برای موارد زیر
        /// <remarks>نام سازمان مربوطه/توضیحات/نام مرکز علمی</remarks>
        /// </summary>
        public  string Description { get; set; }
        ///<summary>
        /// سال آغاز  
        /// </summary>
        public int BeginYear { get; set; }
        /// <summary>
        /// سال پایان 
        /// </summary>
        public int EndYear { get; set; }
        /// <summary>
        /// نوع سابقه آموزشی
        /// </summary>
        public EducationalExperienceType Type { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// استاد مرتبط با سابقه تدریس
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// آی دی استاد مرتبط با سابقه تدریس
        /// </summary>
        public  Guid TeacherId { get; set; }

        /// <summary>
        /// عنوان تدریس شده یا درس
        /// </summary>
        public  Title Title { get; set; }
        /// <summary>
        ///آی دی عنوان تدریس شده یا درس
        /// </summary>
        public  Guid TitleId { get; set; }
        #endregion
    }
}
