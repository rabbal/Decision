using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum AcademicDegrees
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BS))]
        BS = 0,
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MS))]
        MS,
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.PhD))]
        PhD,
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.OtherAcademicDegree))]
        Other
    }
}