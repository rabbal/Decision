using System.Collections.Generic;

namespace Decision.ViewModels.WorkExperience
{
    public class WorkExperienceListViewModel
    {
        public WorkExperienceSearchRequest SearchRequest { get; set; }

        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

    }
}