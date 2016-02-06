using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// نشانی مربوط به منزل است یا محل کار
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// منزل
        /// </summary>
        [Display(Name = "منزل")]
        Home,
        /// <summary>
        /// محل کار
        /// </summary>
        [Display(Name = "محل کار")]
        Office,

    }
}
