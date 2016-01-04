using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Article
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی مقاله های صادر شده توسط استاد
    /// </summary>
    public class ArticleSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی استاد مربوط به مقاله صادر شده
        /// </summary>
         public Guid TeacherId { get; set; }
    }
}