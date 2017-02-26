using System;
using System.Collections.Generic;
using Decision.DomainClasses.Applicants;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class Interview : TrackableEntity<long, User>
    {
        #region Constructor

        public Interview()
        {
            Answers = new HashSet<InterviewAnswer>();
        }
        #endregion

        #region Properties
        public string Content { get; set; }
        public DateTime InterviewDateTime { get; set; }

        #endregion

        #region Navigation Properties
        
        public EvaluationPeriod EvaluationPeriod { get; set; }
        public long EvaluationPeriodId { get; set; }
        public Applicant Applicant { get; set; }
        public long ApplicantId { get; set; }
        public ICollection<InterviewAnswer> Answers { get; set; }
        #endregion
    }
}