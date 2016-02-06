using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
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
