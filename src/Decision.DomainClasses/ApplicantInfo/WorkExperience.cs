using System;

namespace Decision.DomainClasses.ApplicantInfo
{
    public class WorkExperience : BaseEntity
    {
        #region Properties

        public double Score { get; set; }

        public DateTime TenureBeginDate { get; set; }

        public DateTime TenureEndDate { get; set; }

        public string OfficeName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string OffieceAddress { get; set; }

        public string OfficePhoneNumber { get; set; }

        public string ResponsibilityType { get; set; }

        public string OrganizationUnit { get; set; }

        #endregion

        #region NavigationProperties

        public Applicant Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        #endregion
    }
}