using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده وضعیت متقاضی
    /// </summary>
    public enum  ApplicantStatus
    {
        /// <summary>
        /// معلق
        /// </summary>
        [Display(Name = "معلق")]
        Pending,
        /// <summary>
        /// بررسی اولیه توسط دبیرخانه
        /// </summary>
        [Display(Name = "بررسی اولیه")]
        InitialReview,
        /// <summary>
        /// بررسی هیئت جذب
        /// </summary>
        [Display(Name = "بررسی هیئت جذب")]
        InProgress,
        /// <summary>
        /// تأیید شده توسط هیئت جذب
        /// </summary>
        [Display(Name = "تأیید شده توسط هیئت جذب")]
        Approved,
        /// <summary>
        /// پذیرفته شده
        /// </summary>
        [Display(Name = "پذیرفته شده")]
        Accepted
    }
}
