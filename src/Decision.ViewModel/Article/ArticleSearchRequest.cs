using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Article
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی مقاله های صادر شده توسط متقاضی
    /// </summary>
    public class ArticleSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوط به مقاله صادر شده
        /// </summary>
         public Guid ApplicantId { get; set; }
    }
}