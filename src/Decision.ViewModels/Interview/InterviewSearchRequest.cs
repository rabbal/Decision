using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Interview
{
    public class InterviewSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}