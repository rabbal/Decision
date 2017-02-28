using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.GeneralBasicData.ResearchExperience
{
    public class ResearchExperienceSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}