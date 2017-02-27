using System.Collections.Generic;

namespace Decision.ViewModels.GeneralBasicData.WorkExperience
{
    public class WorkExperienceListViewModel
    {
        public WorkExperienceListRequest SearchRequest { get; set; }

        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

    }
}