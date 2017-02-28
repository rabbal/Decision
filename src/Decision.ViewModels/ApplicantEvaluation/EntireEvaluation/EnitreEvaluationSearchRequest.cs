using System;
using Decision.Framework.Domain.Models;

namespace Decision.ViewModels.ApplicantEvaluation.EntireEvaluation
{
    public class EntireEvaluationSearchRequest : ListRequestBase
    {
        public  Guid  ApplicantId { get; set; }

    }
}