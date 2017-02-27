using System;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class ApplicantViewModel 
    {
        #region Properties
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDateTime { get; set; }
        public string NationalCode { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string PhotoFileName { get; set; }
        public string FatherName { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CellPhoneNumber { get; set; }
        public string NumberIndispensable { get; set; }
        public MilitaryStatus MilitaryStatus { get; set; }
        public DateTime? ServedEndDateTime { get; set; }
        public MembershipType MembershipType { get; set; }
        public MarriageStatus MarriageStatus { get; set; }
        public GenderType Gender { get; set; }
        public decimal TotalReputation { get; set; }
        public ApplicantStatus Status { get; set; }
        #endregion
    }
}