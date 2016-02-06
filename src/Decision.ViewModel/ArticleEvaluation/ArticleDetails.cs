using System;

namespace Decision.ViewModel.ArticleEvaluation
{
    public class ArticleDetails
    {
        public string ApplicantFullName { get; set; }
        public string ArticleCode { get; set; }
        public float TotalScore { get; set; }
        public Guid ApplicantId { get; set; }
    }
}
