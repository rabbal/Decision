using System;

namespace Decision.ViewModels.ApplicantEvaluation.EntireEvaluation
{
    public class EntireEvaluationSearchRequest : ListRequestBase
    {
        public  Guid  ApplicantId { get; set; }

    }
}