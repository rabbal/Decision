using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده نوع عضویت
    /// </summary>
    public enum MembershipType
    {
        /// <summary>
        /// پیمانی
        /// </summary>
        [Display(Name = "پیمانی")]
        Contractual,
        /// <summary>
        /// راتبه
        /// </summary>
        [Display(Name = "راتبه")]
        Ratbh,
        /// <summary>
        /// طرح سربازی
        /// </summary>
        [Display(Name = "طرح سربازی")]
        MilitaryPlan
    }
}
