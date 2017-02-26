using System.Collections.Generic;

namespace Decision.ViewModels.ResearchExperience
{
    public class ResearchExperienceListViewModel
    {
        public ResearchExperienceSearchRequest SearchRequest { get; set; }
        public IEnumerable<ResearchExperienceViewModel> ResearchExperiences { get; set; }
    }
}