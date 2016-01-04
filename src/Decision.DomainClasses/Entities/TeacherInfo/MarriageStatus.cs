using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.Entities.TeacherInfo
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
