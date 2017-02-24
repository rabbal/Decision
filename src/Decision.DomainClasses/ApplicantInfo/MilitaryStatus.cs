using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum MilitaryStatus
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Served))]
        Served = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.EducationPardon))]
        EducationPardon,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.PermanentExemption))]
        PermanentExemption,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Serving))]
        Serving
    }
}