using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum CooperationType
    {
        [Display(Name = "مسئول")] Accountable,

        [Display(Name = "همکار")] Coworker
    }
}