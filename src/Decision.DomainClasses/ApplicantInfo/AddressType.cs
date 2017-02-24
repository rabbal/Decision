using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public enum AddressType
    {
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Home))]
        Home = 0,

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Office))]
        Office
    }
}