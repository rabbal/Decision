using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// وضعیت تأهل
    /// </summary>
    public enum MarriageStatus
    {
        /// <summary>
        /// متأهل
        /// </summary>
        [Display(Name = "متأهل")]
        Married,
        /// <summary>
        /// مجرد
        /// </summary>
        [Display(Name = "مجرد")]
        Single,
    }
}
