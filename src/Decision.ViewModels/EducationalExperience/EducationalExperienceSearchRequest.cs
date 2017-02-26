using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EducationalExperience
{
    public class EducationalExperienceSearchRequest : ListRequestBase
    {

        public  Guid ApplicantId { get; set; }
    }
}