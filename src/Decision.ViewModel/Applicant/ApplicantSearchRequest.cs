using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Applicant
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی متقاضی ها
    /// </summary>
    public class ApplicantSearchRequest : BaseSearchRequest
    {
        public ApplicantSearchRequest()
        {
            CurrentSort = SortByMode.CreatedOn;
        }
        /// <summary>
        /// آی دی سمت متقاضی
        /// </summary>
        [DisplayName("سمت")]
        public Guid? PositionId { get; set; }
        /// <summary>
        /// شهر تولد
        /// </summary>
        [DisplayName("شهر تولد")]
        public string City { get; set; }
        /// <summary>
        /// استان تولد
        /// </summary>
        [DisplayName("استان تولد")]
        public string State { get; set; }

        /// <summary>
        /// مرکز کارآموزی
        /// </summary>
        [DisplayName("مرکز کارآموزی")]
        public Guid? TrainingCenter { get; set; }
        /// <summary>
        /// نام متقاضی
        /// </summary>
        [DisplayName("نام")]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        /// <summary>
        /// کد ملی متقاضی
        /// </summary>
        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه متقاضی
        /// </summary>
        [DisplayName("شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }
        /// <summary>
        /// پایه از
        /// </summary>
        [DisplayName("از پایه")]
        public int? CollegiateOrderFrom { get; set; }
        /// <summary>
        /// تا پایه
        /// </summary>
        [DisplayName("تا پایه")]
        public int? CollegiateOrderTo { get; set; }
        /// <summary>
        /// گروه شغلی متقاضی از
        /// </summary>
        [DisplayName("از گروه شغلی")]
        public  int? OccupationalGroupFrom { get; set; }
        /// <summary>
        /// گروه شغلی متقاضی تا
        /// </summary>
        [DisplayName("تا گروه شغلی")]
        public  int? OccupationalGroupTo { get; set; }
   
        /// <summary>
        /// فیلتر بر اساس تأیید شده یا نشده
        /// </summary>
        [DisplayName("وضعیت تأیید")]
        public ApplicantApprovalFilter ApplicantApprovalFilter { get; set; }
        /// <summary>
        /// فیلتر بر اساس ارجاع داده شده یا نشده
        /// </summary>
        [DisplayName("وضعیت ارجاع")]
        public ApplicantReferenceFilter ApplicantReferenceFilter { get; set; }
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonnelCode { get; set; }
    }

    public enum ApplicantApprovalFilter
    {
        /// <summary>
        /// همه
        /// </summary>
        [Display(Name = "همه")]
        All,
        /// <summary>
        /// تأیید شده ها
        /// </summary>
        [Display(Name = "تأیید شده ها")]
        IsApproved,
        /// <summary>
        /// تأیید نشده ها
        /// </summary>
        [Display(Name = "در انتظار تأیید")]
        NonApproved,
    }
    public enum ApplicantReferenceFilter
    {
        /// <summary>
        /// همه
        /// </summary>
        [Display(Name = "همه")]
        All,
        /// <summary>
        /// در ارجاع
        /// </summary>
        [Display(Name = "در ارجاع")]
        Referenced,
        /// <summary>
        /// تثبیت شده ها
        /// </summary>
        [Display(Name = "تثبیت شده ها")]
        NonReferenced,
    }
    public static class ApplicantSortBy
    {
        public const string FirstName = nameof(FirstName);
        public const string LastName = nameof(LastName);
    }
}