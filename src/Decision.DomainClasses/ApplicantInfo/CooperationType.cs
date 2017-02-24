using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// انواع همکاری در پروژه‌ها
    /// </summary>
    public enum CooperationType
    {
        /// <summary>
        /// مسئول
        /// </summary>
        [Display(Name = "مسئول")]
        Accountable,
        /// <summary>
        /// همکار
        /// </summary>
        [Display(Name = "همکار")]
        Coworker
    }
}
