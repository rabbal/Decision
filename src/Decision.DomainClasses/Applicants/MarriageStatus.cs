using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Applicants
{
    public enum MarriageStatus
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Married))]
        Married = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Single))]
        Single
    }
}