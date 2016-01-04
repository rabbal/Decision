using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ArticleEvaluation
{
    public class ArticleEvaluationSearchRequest : BaseSearchRequest
    {
        public Guid ArticleId { get; set; }
        public Guid TeacherId { get; set; }

    }
}
