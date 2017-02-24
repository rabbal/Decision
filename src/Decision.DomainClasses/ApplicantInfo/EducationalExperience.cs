using System;
using Decision.Common.Domain.Tracking;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses.ApplicantInfo
{
    public class EducationalExperience : TrackableEntity<long, User>
    {
        #region Properties

        public string Institution { get; set; }

        public DateTime BeginYear { get; set; }

        public DateTime? EndYear { get; set; }

        public string Lessons { get; set; }

        public string InstitutionAddress { get; set; }

        public string InstitutionPhoneNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public double Score { get; set; }

        #endregion

        #region NavigationProperties

        public Applicant Applicant { get; set; }

        public long ApplicantId { get; set; }

        #endregion
    }
}