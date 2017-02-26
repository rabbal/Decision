using System;
using Decision.DomainClasses.Applicants;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Applicant
{
    public class ApplicantDetailsViewModel:BaseViewModel
    {
        #region Properties
        public long Id { get; set; }
        public  string FullName { get; set; }
        public  DateTime BirthDate { get; set; }
        public  string NationalCode { get; set; }
        public  string BirthCertificateNumber { get; set; }
        public  int CollegiateOrder { get; set; }
        public  int OccupationalGroup { get; set; }
        public  MarriageStatus MarriageStatus { get; set; }
        public  string BankName { get; set; }
        public  string BankBranch { get; set; }
        public  string AccountNumber { get; set; }
        public  bool IsClothed { get; set; }
        public  GenderType Gender { get; set; }
        public  decimal TrainingGPA { get; set; }
        public  int TrainigGrade { get; set; }

        public  byte[] Photo { get; set; }
        public  int OfficialYears { get; set; }
        public  int CollegiateYears { get; set; }
        public  bool IsInReference { get; set; }
        public  bool IsApproved { get; set; }
        public  string BirthPlaceCity { get; set; }
        public  string BirthPlaceState { get; set; }
        public string PersonnelCode { get; set; }
        public  string PositionName { get; set; }
        public  string ApproveByName { get; set; }
        public string  TrainingCourseDetails { get; set; }

        public Guid? TrainingCourseId { get; set; }
        #endregion
    }
}
