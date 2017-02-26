using System;
using System.ComponentModel;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EducationalExperience
{
    public class EducationalExperienceViewModel : BaseViewModel
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        [DisplayName("سال آغاز")]
        public DateTime BeginYear { get; set; }
        [DisplayName("سال پایان")]
        public DateTime? EndYear { get; set; }
        [DisplayName("نام موسسه")]
        public string Institution { get; set; }
        [DisplayName("عنوان درس ها")]
        public string Lessons { get; set; }
        [DisplayName("آدرس موسسه")]
        public string InstitutionAddress { get; set; }
        [DisplayName("تلفن موسسه")]
        public string InstitutionPhoneNumber { get; set; }
        [DisplayName("امتیاز")]
        public double Score { get; set; }
        #endregion
    }
}