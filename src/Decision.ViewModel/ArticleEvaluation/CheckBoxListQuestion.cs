using System;
using System.Collections.Generic;

namespace Decision.ViewModel.ArticleEvaluation
{
    public class CheckBoxListQuestion : BaseQuestion
    {
        public IEnumerable<Guid> OptionIds { get; set; }
    }
}
