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
        /// عنوان مربوط به مقاله
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نام مجله یا سمینار
        /// </summary>
        public string MagazineOrSeminarName { get; set; }
        /// <summary>
        /// نوع مجله یا سمینار
        /// </summary>
        public MagazineOrSeminarType MagazineOrSeminarType { get; set; }
        /// <summary>
        /// نوع مسئولیت متقاضی در مقاله
        /// </summary>
        public ArticleResponsibilityType ResponsibilityType { get; set; }
        /// <summary>
        /// نوع مقاله
        /// </summary>
        public ArticleType Type { get; set; }
        /// <summary>
        /// نوع مقاله اگر نوع انتخابی آن غیره باشد
        /// </summary>
        public string ArticleType { get; set; }
        /// <summary>
        /// خلاصه مقاله 
        /// </summary>
        public string Brief { get; set; }
        /// <summary>
        /// تاریخ چاپ 
        /// </summary>
        public DateTime? PublicatedOn { get; set; }
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
