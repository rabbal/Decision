using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.ApplicantEvaluation.Interview
{
    public class InterviewSearchRequest : ListRequestBase
    {
        public Guid ApplicantId { get; set; }
    }
}