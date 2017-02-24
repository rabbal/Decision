using System;
using System.Collections.Generic;
using Decision.DomainClasses.Evaluations;

namespace Decision.DomainClasses.ApplicantInfo
{
    public class Applicant : BaseEntity
    {
        #region Ctor

        public Applicant()
        {
            Photo = BitConverter.GetBytes(0);
            CopyOfBirthCertificate = Photo = BitConverter.GetBytes(0);
            CopyOfNationalCard = Photo = BitConverter.GetBytes(0);
            Status = ApplicantStatus.Pending;
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FatherName { get; set; }

        public string Gilder { get; set; }

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string CellphoneNumber { get; set; }

        public string NumberIndispensable { get; set; }

        public MilitaryStatus MilitaryStatus { get; set; }

        public DateTime? ServedEndOn { get; set; }


        public MembershipType MembershipType { get; set; }

        public DateTime BirthDate { get; set; }

        public string NationalCode { get; set; }

        public string BirthCertificateNumber { get; set; }

        public MarriageStatus MarriageStatus { get; set; }

        public GenderType Gender { get; set; }

        public byte[] Photo { get; set; }

        public byte[] CopyOfNationalCard { get; set; }

        public byte[] CopyOfBirthCertificate { get; set; }

        public string BirthPlaceCity { get; set; }

        public string BirthPlaceState { get; set; }

        public double TotalReputation { get; set; }

        public ApplicantStatus Status { get; set; }

        #endregion

        #region NavigationProperties

        public ICollection<Article> Articles { get; set; }

        public ICollection<EducationalExperience> EducationalExperiences { get; set; }

        public ICollection<ResearchExperience> ReseachExperiences { get; set; }

        public ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        public ICollection<EducationalBackground> EducationalBackgrounds { get; set; }

        public ICollection<WorkExperience> WorkExperiences { get; set; }

        public ICollection<Interview> Interviews { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Presenter> Presenters { get; set; }

        #endregion
    }
}