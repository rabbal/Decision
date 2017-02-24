using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum GenderType
    {
        [Display(Name = "مرد")] Male,

        [Display(Name = "زن")] FeMale
    }
}