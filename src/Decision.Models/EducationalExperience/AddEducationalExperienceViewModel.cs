using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل درج ساقه آموزشی
    /// </summary>
    public class AddEducationalExperienceViewModel
    {
        #region Properties

        ///<summary>
        /// سال آغاز  
        /// </summary>
        [DisplayName("سال آغاز")]
        [Required(ErrorMessage = "لطفا سال آغاز را وارد کنید")]
        public DateTime BeginYear { get; set; } = DateTime.Now;
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