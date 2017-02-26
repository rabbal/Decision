using System;
using System.Collections.Generic;
using Decision.DomainClasses.Evaluations;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Applicants
{
    public class Applicant : TrackableEntity<long, User>
    {
        #region Constructor
        public Applicant()
        {
            Status = ApplicantStatus.Pending;
            Articles = new HashSet<Article>();
            EducationalBackgrounds = new HashSet<EducationalBackground>();
            ReseachExperiences = new HashSet<ResearchExperience>();
            EntireEvaluations = new HashSet<EntireEvaluation>();
            EducationalBackgrounds = new HashSet<EducationalBackground>();
            WorkExperiences = new HashSet<WorkExperience>();
            Interviews = new HashSet<Interview>();

        }
        #endregion

        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string CellphoneNumber { get; set; }

        public string NumberIndispensable { get; set; }

        public MilitaryStatus MilitaryStatus { get; set; }

        public DateTime? ServedEndDateTime { get; set; }

        public MembershipType MembershipType { get; set; }

        public DateTime BirthDateTime { get; set; }

        public string NationalCode { get; set; }

        public string BirthCertificateNumber { get; set; }

        public MarriageStatus MarriageStatus { get; set; }

        public GenderType Gender { get; set; }

        public string PhotoFileName { get; set; }

        public string CopyOfNationalCardFileName { get; set; }

        public string CopyOfBirthCertificateFileName { get; set; }

        public decimal TotalReputation { get; set; }

        public ApplicantStatus Status { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Article> Articles { get; set; }

        public ICollection<EducationalExperience> EducationalExperiences { get; set; }

        public ICollection<ResearchExperience> ReseachExperiences { get; set; }

        public ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        public ICollection<EducationalBackground> EducationalBackgrounds { get; set; }

        public ICollection<WorkExperience> WorkExperiences { get; set; }

        public ICollection<Interview> Interviews { get; set; }
        public Region BirthPlaceCity { get; set; }
        public long BirthPlaceCityId { get; set; }

        #endregion
    }
}