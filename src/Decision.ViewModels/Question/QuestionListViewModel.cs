using System.Collections.Generic;

namespace Decision.ViewModels.Question
{
    public class QuestionListViewModel
    {
        public QuestionSearchRequest SearchRequest { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}