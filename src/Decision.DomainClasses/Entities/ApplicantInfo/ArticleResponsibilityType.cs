using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده نوع مسئولیت متقاضی در یک مقاله
    /// </summary>
    public enum ArticleResponsibilityType
    {
        /// <summary>
        /// نویسنده مسئول
        /// </summary>
        [Display(Name = "نویسنده مسئول")]
        CorrespondingAuthor,
        /// <summary>
        /// نفر اول
        /// </summary>
        [Display(Name = "نفر اول")]
        FirstOne,
        /// <summary>
        /// همکار
        /// </summary>
        [Display(Name = "همکار")]
        CoWorker,
        /// <summary>
        /// دانشجو
        /// </summary>
        [Display(Name = "دانشجو")]
        Student,
        /// <summary>
        /// هیئت علمی
        /// </summary>
        [Display(Name = "هیئت علمی")]
        ScienceCommittee,
        /// <summary>
        /// غیر هیئت علمی
        /// </summary>
        [Display(Name = "غیر هیئت علمی")]
        NonScienceCommittee

    }
}
