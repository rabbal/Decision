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
    /// نشان دهنده سابقه پژوهشی متقاضی
    /// </summary>
    public class ResearchExperience : BaseEntity
    {
        #region Properties
        /// <summary>
        /// امتیاز 
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// نام موسسه یا مرکز آموزشی و پژوهشی
        /// </summary>
        public string Institution { get; set; }
        ///<summary>
        /// سال آغاز  
        /// </summary>
        public DateTime BeginYear { get; set; }
        /// <summary>
        /// سال پایان 
        /// </summary>
        public DateTime? EndYear { get; set; }
        /// <summary>
        /// عنوان پژوهش ها
        /// </summary>
        public string Lessons { get; set; }
        /// <summary>
        /// آدرس موسسه آموزشی و پژوهشی
        /// </summary>
        public string InstitutionAddress { get; set; }
        /// <summary>
        /// شماره تلفن موسسه آموزشی و پژوهشی
        /// </summary>
        public string InstitutionPhoneNumber { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// متقاضی انجام دهنده پژوهش
        /// </summary>
        public Applicant Applicant { get; set; }
        /// <summary>
        /// آی دی متقاضی انجام دهنده پژوهش
        /// </summary>
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
