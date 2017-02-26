using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Applicants
{
    public class ResearchExperience : TrackableEntity<long, User>
    {
        #region Properties

        public decimal Score { get; set; }

        public string Institution { get; set; }

        public int BeginYear { get; set; }

        public int? EndYear { get; set; }

        public string Lessons { get; set; }

        public string InstitutionAddress { get; set; }

        public string InstitutionPhoneNumber { get; set; }

        #endregion

        #region NavigationProperties

        public Applicant Applicant { get; set; }

        public long ApplicantId { get; set; }

        #endregion
    }
}