using System;
using Decision.DomainClasses.ApplicantInfo;

namespace Decision.DomainClasses.Evaluations
{
    public class Interview : BaseEntity
    {
        #region Properties

        public DateTime InterviewDate { get; set; }

        public string Body { get; set; }

        #endregion

        #region NavigationProperties

        public Guid ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        #endregion
    }
}