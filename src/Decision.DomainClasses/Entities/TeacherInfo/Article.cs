using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Evaluations;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده مقاله داده شده توسط استاد
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
        /// کد مربوط به مقاله
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// محتوای مقاله 
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
        /// آی دی استاد صدرو کننده مقاله
        /// </summary>
        public Guid TeacherId { get; set; }
        /// <summary>
        /// استاد صدور کننده مقاله
        /// </summary>
        public Teacher Teacher { get; set; }
        /// <summary>
        /// ارزیابی های به عمل آمده از مقاله صادر شده
        /// </summary>
        public ICollection<ArticleEvaluation> ArticleEvaluations { get; set; }
        #endregion
    }
}
