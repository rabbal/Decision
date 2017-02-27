using System;
using Decision.DomainClasses.Evaluations;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Applicants
{
    public class ApplicantScore : Entity<Guid>
    {
        #region Properties

        public decimal Score { get; set; }
        #endregion

        #region Navigation Properties

        public EvaluationPeriod EvaluationPeriod { get; set; }
        public long EvaluationPeriodId { get; set; }
        public Applicant Applicant { get; set; }
        public long ApplicantId { get; set; }
        #endregion
    }
}
