using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.Applicant
{
    public class AddApplicantViewModel
    {
        public AddApplicantViewModel()
        {
            BirthDate = DateTime.Now;
        }

        [Required(ErrorMessage = "لطفا نام  را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام باید بین سه تا ۵۰ کاراکتر باشد")]
        [DisplayName("نام")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام متقاضی استفاده کنید")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین سه تا ۵۰ کاراکتر باشد")]
        [DisplayName("نام خانوادگی")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام متقاضی استفاده کنید")]
        public string LastName { get; set; }

        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "لطفا تاریخ تولد را مشخش  کنید")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "لطفا کد ملی را وارد کنید")]
        [DisplayName("کد ملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی وارد شده صحیح نمی باشد")]
        [Remote("IsApplicantNationalCodeExist", nameof(Applicant), "Administrator", ErrorMessage = "یک متقاضی با این کد ملی قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفا شماره شناسنامه را وارد کنید")]
        [StringLength(50, ErrorMessage = "شماره شناسنامه باید کمتر از ۵۰ کاراکتر باشد")]
        public string BirthCertificateNumber { get; set; }

        [DisplayName("وضعیت تأهل")]
        [Required(ErrorMessage = "لطفا وضعیت تأهل متقاضی را مشخص کنید")]
        public MarriageStatus MarriageStatus { get; set; }

        [DisplayName("جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت متقاضی را مشخص کنید")]
        public GenderType Gender { get; set; }
        public string PhotoScan { get; set; }

        [DisplayName("عکس متقاضی")]
        public HttpPostedFileBase PhotoFile { get; set; }

        public string CopyOfNationalCardScan { get; set; }

        [DisplayName("کپی کارت ملی")]
        public HttpPostedFileBase CopyOfNationalCardFile { get; set; }

        [DisplayName("کپی شناسنامه")]
        public HttpPostedFileBase CopyOfBirthCertificateFile { get; set; }
        public string CopyOfBirthCertificateScan { get; set; }
        [DisplayName("شهر محل تولد")]
        public string BirthPlaceCity { get; set; }
        [DisplayName("استان محل تولد")]
        public string BirthPlaceState { get; set; }
        [DisplayName("نام پدر")]
        [Required(ErrorMessage = "لطفا نام پدر متقاضی را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین سه تا ۵۰ کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام پدر متقاضی استفاده کنید")]
        public string FatherName { get; set; }
        [DisplayName("مذهب")]
        [Required(ErrorMessage = "لطفا مذهب متقاضی را وارد کنید")]
        [StringLength(20, ErrorMessage = "مذهب متقاضی نباید بیش از20 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای مذهب متقاضی استفاده کنید")]
        public string Gilder { get; set; }
        [DisplayName("ملیت")]
        [Required(ErrorMessage = "لطفا ملیت متقاضی را وارد کنید")]
        [StringLength(20, ErrorMessage = "ملیت متقاضی نباید بیش از20 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای ملیت متقاضی استفاده کنید")]
        public string Nationality { get; set; }
        
        [DisplayName("شماره تلفن ثابت")]
        [Required(ErrorMessage = "لطفا شماره تلفن ثابت متقاضی را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل متقاضی را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را به شکل صحیح وارد کنید")]
        [DisplayName("ایمیل")]
        [StringLength(256, ErrorMessage = "حداکثر طول ایمیل 256 حرف است")]
       // [Remote("IsEmailExist", nameof(Applicant), "", ErrorMessage = "این ایمیل قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string Email { get; set; }

        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "لطفا شماره همراه متقاضی را وارد کنید")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "لطفا شماره همراه را به شکل صحیح وارد کنید")]
        public string CellphoneNumber { get; set; }

        [DisplayName("شماره تلفن ضروری")]
        [Required(ErrorMessage = "لطفا برای مواقع ضروری یک شماره تلفن وارد کنید")]
        public string NumberIndispensable { get; set; }
        [DisplayName("وضیعت نظام وظیفه")]
        [Required(ErrorMessage = "لطفا وضعیت نظام وظیفه متقاضی را مشخص کنید")]
        public MilitaryStatus MilitaryStatus { get; set; }

        [DisplayName("تاریخ پایان خدمت")]
        public DateTime? ServedEndOn { get; set; }
        [DisplayName("نوع عضویت")]
        [Required(ErrorMessage = "لطفا نوع عضویت متقاضی را مشخص کنید")]
        public MembershipType MembershipType { get; set; }
        public IEnumerable<SelectListItem> StatesForBirthPlace { get; set; }
        public IEnumerable<SelectListItem> CitiesForBirthPlace { get; set; }

    }
}
