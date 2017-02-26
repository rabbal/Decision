using System;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EntireEvaluation
{
    public class EntireEvaluationSearchRequest : ListRequestBase
    {
        public  Guid  ApplicantId { get; set; }

    }
}