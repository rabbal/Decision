using System.Collections.Generic;

namespace Decision.ViewModel.ArticleEvaluation
{
    /// <summary>
    /// ویو مدل نمایش لیست ارزیابی های انجام شده از مقاله استاد ها ها
    /// </summary>
    public class ArticleEvaluationListViewModel
    {
        public ArticleDetails ArticleDetails { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش ارزیابی های انجام شده از مقاله استاد ها
        /// </summary>
        public IEnumerable<ArticleEvaluationViewModel> ArticleEvaluations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ArticleEvaluationSearchRequest Request { get; set; }
    }
}