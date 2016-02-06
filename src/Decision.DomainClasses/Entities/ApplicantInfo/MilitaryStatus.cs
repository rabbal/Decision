using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده وضعیت سربازی
    /// </summary>
    public enum  MilitaryStatus
    {
        /// <summary>
        /// خدمت کرده
        /// </summary>
        [Display(Name = "خدمت کرده")]
        Served,
        /// <summary>
        /// معافیت تحصیلی
        /// </summary>
        [Display(Name = "معافیت تحصیلی")]
        EducationPardon,
        /// <summary>
        /// معاف دائم
        /// </summary>
        [Display(Name = "معاف دائم")]
        PermanentExemption,
        /// <summary>
        /// مشغول خدمت
        /// </summary>
        [Display(Name ="مشغول خدمت")]
        Serving


    }
}
