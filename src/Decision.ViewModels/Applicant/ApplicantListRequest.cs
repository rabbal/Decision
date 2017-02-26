using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Applicant
{
    public class ApplicantListRequest : ListRequestBase
    {
        public ApplicantListRequest()
        {
            CurrentSort = SortByMode.CreatedOn;
        }
        [DisplayName("سمت")]
        public Guid? PositionId { get; set; }
        [DisplayName("شهر تولد")]
        public string City { get; set; }
        [DisplayName("استان تولد")]
        public string State { get; set; }

        [DisplayName("مرکز کارآموزی")]
        public Guid? TrainingCenter { get; set; }
        [DisplayName("نام")]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }
        [DisplayName("شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }
        [DisplayName("از پایه")]
        public int? CollegiateOrderFrom { get; set; }
        [DisplayName("تا پایه")]
        public int? CollegiateOrderTo { get; set; }
        [DisplayName("از گروه شغلی")]
        public  int? OccupationalGroupFrom { get; set; }
        [DisplayName("تا گروه شغلی")]
        public  int? OccupationalGroupTo { get; set; }
   
        [DisplayName("وضعیت تأیید")]
        public ApplicantApprovalFilter ApplicantApprovalFilter { get; set; }
        [DisplayName("وضعیت ارجاع")]
        public ApplicantReferenceFilter ApplicantReferenceFilter { get; set; }
        public string PersonnelCode { get; set; }
    }

    public enum ApplicantApprovalFilter
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "تأیید شده ها")]
        IsApproved,
        [Display(Name = "در انتظار تأیید")]
        NonApproved,
    }
    public enum ApplicantReferenceFilter
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "در ارجاع")]
        Referenced,
        [Display(Name = "تثبیت شده ها")]
        NonReferenced,
    }
    public static class ApplicantSortBy
    {
        public const string FirstName = nameof(FirstName);
        public const string LastName = nameof(LastName);
    }
}