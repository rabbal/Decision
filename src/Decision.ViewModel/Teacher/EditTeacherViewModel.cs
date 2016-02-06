using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.ViewModel.Applicant
{
    /// <summary>
    /// ویومدل ویرایش متقاضی
    /// </summary>
    public class EditApplicantViewModel
    {

        #region Ctor

        public EditApplicantViewModel()
        {
            CitiesForBirthPlace = new List<SelectListItem>();
            StatesForBirthPlace = new List<SelectListItem>();
            StatesForTrainingCeneter = new List<SelectListItem>();
            CitiesForTrainingCeneter = new List<SelectListItem>();
            TrainingCenters = new List<SelectListItem>();
            TrainingCourses = new List<SelectListItem>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی متقاضی
        /// </summary>
        public  Guid Id { get; set; }

        /// <summary>
        /// نام متقاضی
        /// </summary>
        [Required(ErrorMessage = "لطفا نام  را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام باید بین سه تا ۵۰ کاراکتر باشد")]
        [DisplayName("نام")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام متقاضی استفاده کنید")]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی متقاضی
        /// </summary>
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین سه تا ۵۰ کاراکتر باشد")]
        [DisplayName("نام خانوادگی")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام متقاضی استفاده کنید")]
        public string LastName { get; set; }
        /// <summary>
        /// تاریخ تولد متقاضی
        /// </summary>
        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "لطفا تاریخ تولد را مشخش  کنید")]

        public DateTime BirthDate { get; set; }
        /// <summary>
        /// کد ملی متقاضی
        /// </summary>
        [Required(ErrorMessage = "لطفا کد ملی را وارد کنید")]
        [DisplayName("کد ملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی وارد شده صحیح نمی باشد")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا فقط  اعدد انگلیسی استفاده کنید")]
        [Remote("IsApplicantNationalCodeExist", "Applicant", "Administrator", ErrorMessage = "یک متقاضی با این کد ملی قبلا در سیستم ثبت شده است", HttpMethod = "POST",AdditionalFields = "Id")]
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه متقاضی
        /// </summary>
        [DisplayName("شماره شناسنامه")]
        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        [StringLength(50,  ErrorMessage = "شماره شناسنامه باید کمتر از ۵۰ کاراکتر باشد")]

       
        public string BirthCertificateNumber { get; set; }
        /// <summary>
        /// پایه متقاضی
        /// </summary>
        [DisplayName("پایه")]
        [Required(ErrorMessage = "لطفا پایه را وارد کنید")]
        [Range(1, 15, ErrorMessage = "پایه عددی در بازه [۱-۱۵] میباشد")]
        public int CollegiateOrder { get; set; }
        /// <summary>
        /// گروه شغلی متقاضی
        /// </summary>
        [DisplayName("گروه شغلی")]
        [Required(ErrorMessage = "لطفا گروه شغلی را مشخص کنید")]
        [Range(1, 15, ErrorMessage = "گروه شغلی  عددی در بازه [۱-۱۵] میباشد")]
        public int OccupationalGroup { get; set; }
        /// <summary>
        /// وضعیت تأهل متقاضی
        /// </summary>
        [DisplayName("وضعیت تأهل")]
        [Required(ErrorMessage = "لطفا وضعیت تأهل متقاضی را مشخص کنید")]
        public MarriageStatus MarriageStatus { get; set; }
        /// <summary>
        /// نام یک بانکی که متقاضی در آن حساب دارد
        /// </summary>
        [Required(ErrorMessage = "لطفا نام بانک را وارد کنید")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "نام بانک باید بین سه تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("نام بانک")]
        public string BankName { get; set; }
        /// <summary>
        /// نام یک شعبه ای از بانک معرفی شده که متقاضی در آن حساب دارد
        /// </summary>

        [StringLength(256, ErrorMessage = "نام شعبه بانک باید بین سه تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("نام شعبه بانک")]
        public string BankBranch { get; set; }
        /// <summary>
        /// شماره حساب متقاضی در شعبه مربوطه
        /// </summary>
        [StringLength(256, MinimumLength = 3, ErrorMessage = "شماره حساب باید بین سه تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("شماره حساب")]
        public string AccountNumber { get; set; }
        /// <summary>
        /// لباس آخوندی پوشیده است یا خیر؟
        /// </summary>
        [DisplayName("ملبس")]
        public bool IsClothed { get; set; }
        /// <summary>
        /// سمت متقاضی
        /// </summary>
        [DisplayName("سمت متقاضی")]
        public Guid? PositionId { get; set; }
        /// <summary>
        /// جنسیت متقاضی
        /// </summary>
        [DisplayName("جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت متقاضی را مشخص کنید")]
        public GenderType Gender { get; set; }
        /// <summary>
        /// معدل کار آموزی
        /// </summary>
        [DisplayName("معدل کار آموزی")]
        [Required(ErrorMessage = "لطفا معدل کارآموزی را وارد کنید")]
        [Range(0.00, 20.00, ErrorMessage = "معدل کارآموزی میبایست بین صفر تا ۲۰ باشد")]

        public decimal TrainingGPA { get; set; }
        /// <summary>
        /// رتبه کارآموزی
        /// </summary>
        [DisplayName("رتبه کارآموزی")]
        [Required(ErrorMessage = "لطفا رتبه کارآموزی را وارد کنید")]
        
        public int TrainigGrade { get; set; }
        /// <summary>
        ///  عکس متقاضی اسکن شده متقاضی
        /// </summary>
        public string PhotoScan { get; set; }
        /// <summary>
        /// عکس متقاضی
        /// </summary>
        [DisplayName("عکس متقاضی")]
        public HttpPostedFileBase PhotoFile { get; set; }
        /// <summary>
        /// اسکن کپی کارت ملی
        /// </summary>
        public string CopyOfNationalCardScan { get; set; }
        /// <summary>
        /// فایل کارت ملی
        /// </summary>
        [DisplayName("کپی کارت ملی")]
        public HttpPostedFileBase CopyOfNationalCardFile { get; set; }
        /// <summary>
        /// فایل کپی شناسنامه 
        /// </summary>
        [DisplayName("کپی شناسنامه")]
        public HttpPostedFileBase CopyOfBirthCertificateFile { get; set; }
        /// <summary>
        /// اسکن کپی شناسنامه
        /// </summary>
        public string CopyOfBirthCertificateScan { get; set; }
        /// <summary>
        /// سنوات اداری
        /// </summary>
        [DisplayName("سنوات اداری")]
        [Required(ErrorMessage = "لطفا سنوات اداری را  وارد کنید")]
        public int OfficialYears { get; set; }
        /// <summary>
        /// سنوات
        /// </summary>
        [DisplayName("سنوات")]
        [Required(ErrorMessage = "لطفا سنوات را وارد کنید")]
        public int CollegiateYears { get; set; }

        /// <summary>
        /// آی دی دوره  کارآموزی ای که  متقاضی شرکت کرده
        /// </summary>
        [DisplayName("دوره کارآموزی")]
        public Guid? TrainingCourseId { get; set; }
        /// <summary>
        /// شهر محل تولد
        /// </summary>
        [DisplayName("شهر محل تولد")]

        public string BirthPlaceCity { get; set; }
        /// <summary>
        /// استان محل تولد
        /// </summary>
        [DisplayName("استان محل تولد")]

        public string BirthPlaceState { get; set; }
        /// <summary>
        /// کد پرسنلی
        /// </summary>
        [Required(ErrorMessage = "لطفا کد پرسنلی  را وارد کنید")]
        [StringLength(50, ErrorMessage = "کد پرسنلی نباید بیشتر از ۵۰  کاراکتر باشد")]
        [DisplayName("کد پرسنلی")]
        public string PersonnelCode { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست استان ها برای محل تولد
        /// </summary>
        public IEnumerable<SelectListItem> StatesForBirthPlace { get; set; }
        /// <summary>
        /// لیست شهر ها برای محل تولد
        /// </summary>
        public IEnumerable<SelectListItem> CitiesForBirthPlace { get; set; }
        /// <summary>
        /// لیست استان ها برای مرکز کارآموزی
        /// </summary>
        public IEnumerable<SelectListItem> StatesForTrainingCeneter { get; set; }
        /// <summary>
        /// لیست شهر ها برای مرکز کارآموزی
        /// </summary>
        public IEnumerable<SelectListItem> CitiesForTrainingCeneter { get; set; }
        /// <summary>
        /// لیست مراکز کارآموزی برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> TrainingCenters { get; set; }
        /// <summary>
        /// لیست سمت ها برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> Positions { get; set; }
        /// <summary>
        /// لیست دوره ها
        /// </summary>
        public IEnumerable<SelectListItem> TrainingCourses { get; set; }

        /// <summary>
        /// آی دی مرکز کارآموزی مربوط به دوره کارآموزی متقاضی
        /// </summary>
        [DisplayName("مرکز کارآموزی")]
        public  Guid? TrainingCenterId { get; set; }
        /// <summary>
        /// آی دی شهر مربوط به مرکز کارآموزی ای که متقاضی در آن دوره دیده
        /// </summary>
        [DisplayName("شهر محل کارآموزی")]
        public  string TrainingCenterCity { get; set; }
        /// <summary>
        /// آی دی استان مربوط به مرکز کارآموزی ای که متقاضی در آن دوره دیده
        /// </summary>
        [DisplayName("استان محل کارآموزی")]
        public  string TrainingCenterState { get; set; }

        public string FullName => $"{this.FirstName}  {this.LastName}";

        #endregion
    }
}