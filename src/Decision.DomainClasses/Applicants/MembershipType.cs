using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Applicants
{
    public enum MembershipType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Contractual))]
        Contractual = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Ratbh))]
        Ratbh,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.MilitaryPlan))]
        MilitaryPlan
    }
}