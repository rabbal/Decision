using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum MarriageStatus
    {
        [Display(Name = "متأهل")] Married,

        [Display(Name = "مجرد")] Single
    }
}