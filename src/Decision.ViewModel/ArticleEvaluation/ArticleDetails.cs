using System;

namespace Decision.ViewModel.ArticleEvaluation
{
    public class ArticleDetails
    {
        public string TeacherFullName { get; set; }
        public string ArticleCode { get; set; }
        public float TotalScore { get; set; }
        public Guid TeacherId { get; set; }
    }
}
