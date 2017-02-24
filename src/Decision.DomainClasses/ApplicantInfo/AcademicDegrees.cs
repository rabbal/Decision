using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum AcademicDegrees
    {
        [Display(Name = "کارشناسی")] BS,
        [Display(Name = "کارشناسی ارشد")] MS,
        [Display(Name = "دکتری")] PhD,
        [Display(Name = "دوره های تخصصی دیگر")] Other
    }
}