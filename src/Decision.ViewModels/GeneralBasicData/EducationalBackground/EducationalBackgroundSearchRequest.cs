using System;

namespace Decision.ViewModels.GeneralBasicData.EducationalBackground
{
    public class EducationalBackgroundSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}