using System;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Applicants
{
    public class EducationalBackground : TrackableEntity<long, User>
    {
        #region Properties

        public decimal Score { get; set; }

        public AcademicDegrees AcademicDegree { get; set; }

        public string ThesisTopic { get; set; }

        public DateTime GraduationDateTime { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string Advisor { get; set; }

        public string Supervisor { get; set; }

        public string Description { get; set; }

        public decimal GPA { get; set; }

        public decimal ThesisScore { get; set; }

        public string University { get; set; }

        public string Field { get; set; }

        #endregion

        #region NavigationProperties

        public long ApplicantId { get; set; }

        public Applicant Applicant { get; set; }
        public Region Country { get; set; }
        public long CountryId { get; set; }

        #endregion
    }
}