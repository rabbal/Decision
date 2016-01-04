using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// مقاطع تحصیلات حوزوی
    /// </summary>
    public enum HozeDegrees
    {
        /// <summary>
        /// سطح یک
        /// </summary>
       [Display(Name="سطح یک")]
        Level1,
        /// <summary>
        /// سطح دو
        /// </summary>
       [Display(Name = "سطح دو")]
        Level2,
        /// <summary>
        /// سطح سه
        /// </summary>
        [Display(Name = "سطح سه")]
        Level3,
        /// <summary>
        /// سطح چهار
        /// </summary>
        [Display(Name = "سطح چهار")]
        Level4,
        /// <summary>
        /// اجتهاد
        /// </summary>
        [Display(Name = "اجتهاد")]
        Ijtihad
    }
}
