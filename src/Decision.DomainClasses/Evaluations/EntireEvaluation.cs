using System;
using Decision.DomainClasses.ApplicantInfo;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class EntireEvaluation : TrackableEntity<long, User>
    {
        #region Properties

        public string Content { get; set; }

        public DateTime EvaluationDate { get; set; }

        public string Foible { get; set; }

        public string StrongPoint { get; set; }

        #endregion

        #region NavigationProperties

        public long ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        #endregion
    }
}