using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// انواع تحصیلات
    /// </summary>
    public enum EducationalType
    {
        /// <summary>
        /// حوزوی
        /// </summary>
        [Display(Name = "حوزوی")]
        Hoze,
        /// <summary>
        /// دانشگاهی
        /// </summary>
        [Display(Name = "دانشگاهی")]
        Academic
    }
}
