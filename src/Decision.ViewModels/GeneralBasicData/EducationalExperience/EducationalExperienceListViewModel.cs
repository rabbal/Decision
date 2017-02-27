using System.Collections.Generic;

namespace Decision.ViewModels.GeneralBasicData.EducationalExperience
{
    public class EducationalExperienceListViewModel
    {
        public EducationalExperienceSearchRequest SearchRequest { get; set; }

        public IEnumerable<EducationalExperienceViewModel> EducationalExperiences { get; set; }

    }
}