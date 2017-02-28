using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.GeneralBasicData.WorkExperience
{
    public class WorkExperienceListRequest : ListRequestBase
    {
        public long ApplicantId { get; set; }
    }
}