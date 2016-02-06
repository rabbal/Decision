using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده مقاله داده شده توسط متقاضی
    /// </summary>
    public class Article : BaseEntity
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Article()
        {
            Attachment = BitConverter.GetBytes(0);
        }
        #endregion

        #region Properties
        /// <summary>
        /// امتیاز 
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// موضوع مربوط به مقاله
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// خلاصه مقاله
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// خلاصه مقاله 
        /// </summary>
        public string Brief { get; set; }
        /// <summary>
        /// تاریخ ارائه مقاله
        /// </summary>
        public DateTime ArticleDate { get; set; }
        /// <summary>
        /// فایل ضمیمه  مقاله صدور شده
        /// </summary>
        public byte[] Attachment { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی متقاضی صدرو کننده مقاله
        /// </summary>
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// متقاضی صدور کننده مقاله
        /// </summary>
        public Applicant Applicant { get; set; }
        #endregion
    }
}
