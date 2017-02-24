using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل نمایش سابقه آموزشی
    /// </summary>
    public class EducationalExperienceViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی سابقه آموزشی
        /// </summary>
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        ///<summary>
        /// سال آغاز  
        /// </summary>
        [DisplayName("سال آغاز")]
        public DateTime BeginYear { get; set; }
        /// <summary>
        /// سال پایان 
        /// </summary>
        [DisplayName("سال پایان")]
        public DateTime? EndYear { get; set; }
        /// <summary>
        /// نام موسسه یا مرکز آموزشی
        /// </summary>
        [DisplayName("نام موسسه")]
        public string Institution { get; set; }
        /// <summary>
        /// عنوان درس های تدریس شده یا درحال تدریس
        /// </summary>
        [DisplayName("عنوان درس ها")]
        public string Lessons { get; set; }
        /// <summary>
        /// آدرس موسسه آموزشی
        /// </summary>
        [DisplayName("آدرس موسسه")]
        public string InstitutionAddress { get; set; }
        /// <summary>
        /// شماره تلفن موسسه آموزشی
        /// </summary>
        [DisplayName("تلفن موسسه")]
        public string InstitutionPhoneNumber { get; set; }
        /// <summary>
        /// امتیاز 
        /// </summary>
        [DisplayName("امتیاز")]
        public double Score { get; set; }
        #endregion
    }
}