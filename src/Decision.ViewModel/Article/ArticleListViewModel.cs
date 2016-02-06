using System.Collections.Generic;

namespace Decision.ViewModel.Article
{
    /// <summary>
    /// ویو مدل نمایش لیست مقاله های صادر شده توسط متقاضی
    /// </summary>
    public class ArticleListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public ArticleSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش مقاله های صادر شده توسط متقاضی
        /// </summary>
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}