using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مقاطع تحصیلات دانشگاهی
    /// </summary>
    public enum  AcademicDegrees
    {
        /// <summary>
        ///  لیسانس 
        /// </summary>
        [Display(Name = "کارشناسی")]
        BS,
        /// <summary>
        /// فوق لیسانس
        /// </summary>
        [Display(Name = "کارشناسی ارشد")]
        MS,
        /// <summary>
        /// دکتری
        /// </summary>
       [Display(Name = "دکتری")]
        PhD,
        /// <summary>
        /// دوره های تخصصی دیگر
        /// </summary>
        [Display(Name = "دوره های تخصصی دیگر")]
        Other
    }
}
