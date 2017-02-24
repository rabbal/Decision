using System;
using Decision.Common.Domain.Tracking;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses.ApplicantInfo
{
    public class EducationalBackground : TrackableEntity<long, User>
    {
        #region Properties

        public double Score { get; set; }

        public AcademicDegrees AcademicDegree { get; set; }

        public string ThesisTopic { get; set; }

        public DateTime GraduationDate { get; set; }

        public DateTime EntryDate { get; set; }

        public string Advisor { get; set; }

        public string Supervisor { get; set; }

        public string Description { get; set; }

        public decimal GPA { get; set; }

        public decimal ThesisScore { get; set; }

        public string Country { get; set; }

        public string University { get; set; }

        public string Field { get; set; }

        #endregion

        #region NavigationProperties

        public long ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        #endregion
    }
}