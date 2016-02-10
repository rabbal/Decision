using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل ویرایش سابقه آموزشی
    /// </summary>
    public class EditEducationalExperienceViewModel:BaseRowVersion
    {

        #region Properties
        /// <summary>
        /// آی دی سابقه آموزشی
        /// </summary>
        public Guid Id { get; set; }

        ///<summary>
        /// سال آغاز  
        /// </summary>
        [DisplayName("سال آغاز")]
        [Required(ErrorMessage = "لطفا سال آغاز را وارد کنید")]
        public DateTime BeginYear { get; set; } 

        /// <summary>
        /// سال پایان 
        /// </summary>
        [DisplayName("سال پایان")]
        [Required(ErrorMessage = "لطفا سال پایان را وارد کنید")]
        public DateTime? EndYear { get; set; } 
        /// <summary>
        /// آی دی متقاضی مرتبط با سابقه تدریس
        /// </summary>
        [Required]
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// نام موسسه یا مرکز آموزشی
        /// </summary>
        [DisplayName("نام موسسه")]
        [Required(ErrorMessage = "لطفا نام موسسه را وارد کنید")]
        public string Institution { get; set; }
        /// <summary>
        /// عنوان درس های تدریس شده یا درحال تدریس
        /// </summary>
        [DisplayName("عنوان درس ها")]
        [Required(ErrorMessage = "لطفا درس های تدریس شده را وارد کنید")]
        public string Lessons { get; set; }
        /// <summary>
        /// آدرس موسسه آموزشی
        /// </summary>
        [DisplayName("آدرس موسسه")]
        [Required(ErrorMessage = "لطفا آدرس موسسه را وارد کنید")]
        public string InstitutionAddress { get; set; }
        /// <summary>
        /// شماره تلفن موسسه آموزشی
        /// </summary>
        [DisplayName("تلفن موسسه")]
        [Required(ErrorMessage = "لطفا تلفن موسسه را وارد کنید")]
        public string InstitutionPhoneNumber { get; set; }
        /// <summary>
        /// امتیاز 
        /// </summary>
        [DisplayName("امتیاز")]
        [Required(ErrorMessage = "لطفا امتیاز را وارد کنید")]
        public double Score { get; set; }
        #endregion
    }
}