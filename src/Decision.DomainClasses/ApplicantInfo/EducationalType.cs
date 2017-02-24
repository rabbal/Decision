using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum EducationalType
    {
        [Display(Name = "حوزوی")] Hoze,

        [Display(Name = "دانشگاهی")] Academic
    }
}