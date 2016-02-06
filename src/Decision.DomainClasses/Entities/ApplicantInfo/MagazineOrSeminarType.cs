using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
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
