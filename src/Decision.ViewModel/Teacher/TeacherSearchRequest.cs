using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Teacher
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی استاد ها
    /// </summary>
    public class TeacherSearchRequest : BaseSearchRequest
    {
        public TeacherSearchRequest()
        {
            CurrentSort = TeacherSortBy.CreateDate;
        }
        /// <summary>
        /// آی دی سمت استاد
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
        /// نام استاد
        /// </summary>
        [DisplayName("نام")]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }
        /// <summary>
        /// کد ملی استاد
        /// </summary>
        [DisplayName("کد ملی")]
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه استاد
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
        /// گروه شغلی استاد از
        /// </summary>
        [DisplayName("از گروه شغلی")]
        public  int? OccupationalGroupFrom { get; set; }
        /// <summary>
        /// گروه شغلی استاد تا
        /// </summary>
        [DisplayName("تا گروه شغلی")]
        public  int? OccupationalGroupTo { get; set; }
   
        /// <summary>
        /// فیلتر بر اساس تأیید شده یا نشده
        /// </summary>
        [DisplayName("وضعیت تأیید")]
        public TeacherApprovalFilter TeacherApprovalFilter { get; set; }
        /// <summary>
        /// فیلتر بر اساس ارجاع داده شده یا نشده
        /// </summary>
        [DisplayName("وضعیت ارجاع")]
        public TeacherReferenceFilter TeacherReferenceFilter { get; set; }
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonnelCode { get; set; }
    }

    public enum TeacherApprovalFilter
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
    public enum TeacherReferenceFilter
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
    public static class TeacherSortBy
    {
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string CollegiateOrder = "CollegiateOrder";
        public const string OccupationalGroup = "OccupationalGroup";
        public const string CreateDate = "CreateDate";
        public const string LastModifiedDate = "LastModifiedDate";
    }
}