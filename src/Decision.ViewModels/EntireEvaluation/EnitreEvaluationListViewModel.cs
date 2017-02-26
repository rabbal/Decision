using System.Collections.Generic;

namespace Decision.ViewModels.EntireEvaluation
{
    public class EntireEvaluationListViewModel
    {
        public EntireEvaluationSearchRequest SearchRequest { get; set; }
        public IEnumerable<EntireEvaluationViewModel> EntireEvaluations { get; set; }
    }
}