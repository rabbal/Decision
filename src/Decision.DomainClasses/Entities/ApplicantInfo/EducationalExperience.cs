using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده سوابق آموزشی متقاضی
    /// </summary>
    public class EducationalExperience : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام موسسه یا مرکز آموزشی
        /// </summary>
        public  string Institution { get; set; }
        ///<summary>
        /// سال آغاز  
        /// </summary>
        public DateTime BeginYear { get; set; }
        /// <summary>
        /// سال پایان 
        /// </summary>
        public DateTime? EndYear { get; set; }
        /// <summary>
        /// عنوان درس های تدریس شده یا درحال تدریس
        /// </summary>
        public string Lessons { get; set; }
        /// <summary>
        /// آدرس موسسه آموزشی
        /// </summary>
        public string InstitutionAddress { get; set; }
        /// <summary>
        /// شماره تلفن موسسه آموزشی
        /// </summary>
        public string InstitutionPhoneNumber { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// امتیاز 
        /// </summary>
        public double Score { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// متقاضی مرتبط با سابقه تدریس
        /// </summary>
        public  Applicant Applicant { get; set; }
        /// <summary>
        /// آی دی متقاضی مرتبط با سابقه تدریس
        /// </summary>
        public  Guid ApplicantId { get; set; }
        #endregion
    }
}
