using System;
using Decision.Common.Domain.Tracking;
using Decision.DomainClasses.ApplicantInfo;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses.Evaluations
{
    public class Interview : TrackableEntity<long, User>
    {
        #region Properties

        public DateTime InterviewDate { get; set; }

        public string Body { get; set; }

        #endregion

        #region NavigationProperties

        public long ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        #endregion
    }
}