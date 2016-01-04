using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.TeacherInfo
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
