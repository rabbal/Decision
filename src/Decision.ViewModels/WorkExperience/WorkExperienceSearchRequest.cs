using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.WorkExperience
{
    public class WorkExperienceSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}