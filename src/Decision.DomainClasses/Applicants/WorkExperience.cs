using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Applicants
{
    public class WorkExperience : TrackableEntity<long, User>
    {
        #region Properties

        public decimal Score { get; set; }

        public DateTime TenureBeginDateTime { get; set; }

        public DateTime TenureEndDateTime { get; set; }

        public string OfficeName { get; set; }

        public string OffieceAddress { get; set; }

        public string OfficePhoneNumber { get; set; }

        public string ResponsibilityType { get; set; }

        public string OrganizationUnit { get; set; }

        #endregion

        #region NavigationProperties

        public Applicant Applicant { get; set; }

        public long ApplicantId { get; set; }

        #endregion
    }
}