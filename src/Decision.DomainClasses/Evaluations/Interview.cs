using System;
using Decision.DomainClasses.ApplicantInfo;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

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