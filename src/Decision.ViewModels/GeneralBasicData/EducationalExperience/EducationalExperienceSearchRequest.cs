using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.GeneralBasicData.EducationalExperience
{
    public class EducationalExperienceSearchRequest : ListRequestBase
    {

        public  Guid ApplicantId { get; set; }
    }
}