using System.ComponentModel.DataAnnotations;

namespace Decision.DomainClasses.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده نوع مجله یا سمینار
    /// </summary>
    public enum MagazineOrSeminarType
    {
        /// <summary>
        /// داخلی عملی پژوهشی
        /// </summary>
        [Display(Name = "داخلی علمی پژوهشی")]
        InternalResearch,
        /// <summary>
        /// داخلی غیر علمی پژوهشی
        /// </summary>
        [Display(Name = "داخلی غیر علمی پژوهشی")]
        InternaNonResearch,
        /// <summary>
        /// خارجی ایندکس شده
        /// </summary>
        [Display(Name = "خارجی ایندکس شده")]
        ExternalIndexed,
        /// <summary>
        /// خارجی ایندکس نشده
        /// </summary>
        [Display(Name = "خارجی ایندکس شده")]
        ExternalNotIndexed

    }
}
