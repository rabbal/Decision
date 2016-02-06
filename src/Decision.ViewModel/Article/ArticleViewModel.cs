using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Article
{
    /// <summary>
    /// ویومدل نمایش مقاله های صادر شده توسط متقاضی
    /// </summary>
    public class ArticleViewModel :BaseViewModel
    {
        #region Properties
        [DisplayName("کد مقاله")]
        public string Code { get; set; }
        /// <summary>
        /// آی دی مقاله صادر شده توسط متقاضی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// محتوای مقاله 
        /// </summary>
        [DisplayName("محتوای مقاله")]
        public  string Content { get; set; }

        /// <summary>
        /// خلاصه مقاله 
        /// </summary>
        [DisplayName("خلاصه مقاله")]
        public  string Brief { get; set; }

        /// <summary>
        /// تاریخ ارائه مقاله
        /// </summary>
        [DisplayName("تاریخ ارائه")]
        public  DateTime ArticleDate { get; set; }

        public Guid ApplicantId { get; set; }

        #endregion 
    }
}