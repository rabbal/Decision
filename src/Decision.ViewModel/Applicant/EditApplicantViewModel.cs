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
        [Remote("IsApplicantNationalCodeExist", nameof(Applicant), "Administrator", ErrorMessage = "یک متقاضی با این کد ملی قبلا در سیستم ثبت شده است", HttpMethod = "POST",AdditionalFields = nameof(Id))]
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه متقاضی
        /// </summary>
        [DisplayName("شماره شناسنامه")]
        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        [StringLength(50,  ErrorMessage = "شماره شناسنامه باید کمتر از ۵۰ کاراکتر باشد")]
        public string BirthCertificateNumber { get; set; }
       
        /// <summary>
        /// وضعیت تأهل متقاضی
        /// </summary>
        [DisplayName("وضعیت تأهل")]
        [Required(ErrorMessage = "لطفا وضعیت تأهل متقاضی را مشخص کنید")]
        public MarriageStatus MarriageStatus { get; set; }
        
        /// <summary>
        /// جنسیت متقاضی
        /// </summary>
        [DisplayName("جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت متقاضی را مشخص کنید")]
        public GenderType Gender { get; set; }
       
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
        /// نام پدر
        /// </summary>
        [DisplayName("نام پدر")]
        [Required(ErrorMessage = "لطفا نام پدر متقاضی را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین سه تا ۵۰ کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام پدر متقاضی استفاده کنید")]
        public string FatherName { get; set; }
        /// <summary>
        /// مذهب
        /// </summary>
        [DisplayName("مذهب")]
        [Required(ErrorMessage = "لطفا مذهب متقاضی را وارد کنید")]
        [StringLength(20, ErrorMessage = "مذهب متقاضی نباید بیش از20 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای مذهب متقاضی استفاده کنید")]
        public string Gilder { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        [DisplayName("ملیت")]
        [Required(ErrorMessage = "لطفا ملیت متقاضی را وارد کنید")]
        [StringLength(20, ErrorMessage = "ملیت متقاضی نباید بیش از20 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای ملیت متقاضی استفاده کنید")]
        public string Nationality { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        [DisplayName("شماره تلفن ثابت")]
        [Required(ErrorMessage = "لطفا شماره تلفن ثابت متقاضی را وارد کنید")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        [Required(ErrorMessage = "لطفا ایمیل متقاضی را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        [DisplayName("ایمیل")]
        [StringLength(256, ErrorMessage = "حداکثر طول ایمیل 256 حرف است")]
         //[Remote("IsEmailExist", nameof(Applicant), "", ErrorMessage = "این ایمیل قبلا در سیستم ثبت شده است", HttpMethod = "POST",AdditionalFields ="Id")]
        public string Email { get; set; }
        /// <summary>
        /// تلفن همراه
        /// </summary>
        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا شماره همراه متقاضی را وارد کنید")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه را به شکل صحیح وارد کنید")]
        public string CellphoneNumber { get; set; }
        /// <summary>
        /// شماره تلفن ضروری
        /// </summary>
        [DisplayName("شماره تلفن ضروری")]
        [Required(ErrorMessage = "لطفا برای مواقع ضروری یک شماره تلفن وارد کنید")]
        public string NumberIndispensable { get; set; }
        /// <summary>
        /// وضعیت نظام وظیفه
        /// </summary>
        [DisplayName("وضیعت نظام وظیفه")]
        [Required(ErrorMessage = "لطفا وضعیت نظام وظیفه متقاضی را مشخص کنید")]
        public MilitaryStatus MilitaryStatus { get; set; }
        /// <summary>
        /// تاریخ پایان خدمت
        /// </summary>
        [DisplayName("تاریخ پایان خدمت")]
        public DateTime? ServedEndOn { get; set; }
        /// <summary>
        /// نوع عضویت
        /// </summary>
        [DisplayName("نوع عضویت")]
        [Required(ErrorMessage = "لطفا نوع عضویت متقاضی را مشخص کنید")]
        public MembershipType MembershipType { get; set; }
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
      
        public string FullName => $"{this.FirstName}  {this.LastName}";

        #endregion

    }
}