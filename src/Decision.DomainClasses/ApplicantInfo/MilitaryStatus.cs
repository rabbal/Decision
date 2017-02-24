using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
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
