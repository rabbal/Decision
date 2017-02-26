using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EducationalBackground
{
    public class EducationalBackgroundSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}