using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// انواع جنسیت
    /// </summary>
    public enum  GenderType
    {
        /// <summary>
        /// مذکر
        /// </summary>
        [Display(Name = "مرد")]
        Male,
        /// <summary>
        /// مونث
        /// </summary>
        [Display(Name = "زن")]
        FeMale
    }
}
