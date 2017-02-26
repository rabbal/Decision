using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EducationalExperience
{
    public class EditEducationalExperienceViewModel:BaseRowVersion
    {

        #region Properties
        public Guid Id { get; set; }

        [DisplayName("سال آغاز")]
        [Required(ErrorMessage = "لطفا سال آغاز را وارد کنید")]
        public DateTime BeginYear { get; set; } 

        [DisplayName("سال پایان")]
        [Required(ErrorMessage = "لطفا سال پایان را وارد کنید")]
        public DateTime? EndYear { get; set; } 
        [Required]
        public Guid ApplicantId { get; set; }
        [DisplayName("نام موسسه")]
        [Required(ErrorMessage = "لطفا نام موسسه را وارد کنید")]
        public string Institution { get; set; }
        [DisplayName("عنوان درس ها")]
        [Required(ErrorMessage = "لطفا درس های تدریس شده را وارد کنید")]
        public string Lessons { get; set; }
        [DisplayName("آدرس موسسه")]
        [Required(ErrorMessage = "لطفا آدرس موسسه را وارد کنید")]
        public string InstitutionAddress { get; set; }
        [DisplayName("تلفن موسسه")]
        [Required(ErrorMessage = "لطفا تلفن موسسه را وارد کنید")]
        public string InstitutionPhoneNumber { get; set; }
        [DisplayName("امتیاز")]
        [Required(ErrorMessage = "لطفا امتیاز را وارد کنید")]
        public double Score { get; set; }
        #endregion
    }
}