using System.Collections.Generic;

namespace Decision.ViewModels.GeneralBasicData.ResearchExperience
{
    public class ResearchExperienceListViewModel
    {
        public ResearchExperienceSearchRequest SearchRequest { get; set; }
        public IEnumerable<ResearchExperienceViewModel> ResearchExperiences { get; set; }
    }
}