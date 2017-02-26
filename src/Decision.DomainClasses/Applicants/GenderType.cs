using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Applicants
{
    public enum GenderType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Male))]
        Male = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.FeMale))]
        FeMale
    }
}