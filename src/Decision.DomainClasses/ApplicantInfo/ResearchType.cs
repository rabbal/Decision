using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum ResearchType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.None))]
        None = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.General))]
        General,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Academic))]
        Academic
    }
}