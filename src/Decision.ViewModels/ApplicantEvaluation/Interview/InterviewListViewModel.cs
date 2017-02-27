using System.Collections.Generic;

namespace Decision.ViewModels.ApplicantEvaluation.Interview
{
    public class InterviewListViewModel
    {
        public InterviewSearchRequest SearchRequest { get; set; }

        public IEnumerable<InterviewViewModel> Interviews { get; set; }
    }
}