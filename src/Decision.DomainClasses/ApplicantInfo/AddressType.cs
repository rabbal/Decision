using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{

    public enum AddressType
    {

        [Display(Name = "منزل")]
        Home,

        [Display(Name = "محل کار")]
        Office,

    }
}
