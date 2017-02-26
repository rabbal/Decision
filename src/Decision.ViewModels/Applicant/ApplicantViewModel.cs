using System;
using Decision.DomainClasses.Applicants;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Applicant
{
    public class ApplicantViewModel : BaseViewModel
    {
        #region Properties

        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalCode { get; set; }
        public string BirthCertificateNumber { get; set; }
        public byte[] Photo { get; set; }
        public string BirthPlaceCity { get; set; }
        public string BirthPlaceState { get; set; }
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
        public MarriageStatus MarriageStatus { get; set; }
        public GenderType Gender { get; set; }
        public double TotalReputation { get; set; }
        public ApplicantStatus Status { get; set; }
        #endregion 
    }
}