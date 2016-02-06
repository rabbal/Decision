using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
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
