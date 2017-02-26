using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.ResearchExperience
{
    public class ResearchExperienceSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}