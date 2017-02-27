using System.Collections.Generic;

namespace Decision.ViewModels.ApplicantEvaluation.EntireEvaluation
{
    public class EntireEvaluationListViewModel
    {
        public EntireEvaluationSearchRequest SearchRequest { get; set; }
        public IEnumerable<EntireEvaluationViewModel> EntireEvaluations { get; set; }
    }
}