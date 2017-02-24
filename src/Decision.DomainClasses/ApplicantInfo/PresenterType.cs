using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// انواع پژوهش
    /// </summary>
    public enum PresenterType
    {
        /// <summary>
        /// عمومی
        /// </summary>
       [Display(Name = "عمومی")]
        General , 
        /// <summary>
        /// علمی
        /// </summary>
        [Display(Name = "علمی")]
        Academic
    }
}
