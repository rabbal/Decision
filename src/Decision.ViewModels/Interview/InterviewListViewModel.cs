using System.Collections.Generic;

namespace Decision.ViewModels.Interview
{
    public class InterviewListViewModel
    {
        public InterviewSearchRequest SearchRequest { get; set; }

        public IEnumerable<InterviewViewModel> Interviews { get; set; }
    }
}