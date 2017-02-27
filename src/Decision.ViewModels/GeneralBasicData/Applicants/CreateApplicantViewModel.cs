using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.Applicants
{
    public class CreateApplicantViewModel
    {
        [Required(ErrorMessage = "نام  را وارد کنید.")]
        [StringLength(50, ErrorMessage = "نام باید حداکثر ۵۰ کاراکتر باشد.")]
        [DisplayName("نام")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "فقط از حروف  فارسی برای نام استفاده کنید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی را وارد کنید.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید حداکثر ۵۰ کاراکتر باشد.")]
        [DisplayName("نام خانوادگی")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "فقط ازاعداد و حروف  فارسی برای نام متقاضی استفاده کنید.")]
        public string LastName { get; set; }

        [DisplayName("تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد را مشخش  کنید.")]
        public DateTime BirthDateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "کد ملی را وارد کنید.")]
        [DisplayName("کد ملی")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی وارد شده صحیح نمی باشد.")]
        //[Remote("IsNationalCodeExist", nameof(Applicant), "Administrator", ErrorMessage = "یک متقاضی با این کد ملی قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "شماره شناسنامه را وارد کنید.")]
        [StringLength(50, ErrorMessage = "شماره شناسنامه باید حداکثر ۵۰ کاراکتر باشد.")]
        public string BirthCertificateNumber { get; set; }

        [DisplayName("وضعیت تأهل")]
        [Required(ErrorMessage = "وضعیت تأهل متقاضی را مشخص کنید.")]
        public MarriageStatus MarriageStatus { get; set; }

        [DisplayName("جنسیت")]
        [Required(ErrorMessage = "جنسیت متقاضی را مشخص کنید.")]
        public GenderType Gender { get; set; }

        [DisplayName("عکس متقاضی")]
        public HttpPostedFileBase PhotoFile { get; set; }

        [DisplayName("کپی کارت ملی")]
        public HttpPostedFileBase CopyOfNationalCardFile { get; set; }

        [DisplayName("کپی شناسنامه")]
        public HttpPostedFileBase CopyOfBirthCertificateFile { get; set; }

        [DisplayName("نام پدر")]
        [Required(ErrorMessage = "نام پدر متقاضی را وارد کنید.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام پدر باید حداکثر ۵۰ کاراکتر باشد.")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "فقط از حروف  فارسی برای نام پدر متقاضی استفاده کنید.")]
        public string FatherName { get; set; }
        [DisplayName("مذهب")]
        [Required(ErrorMessage = "مذهب متقاضی را وارد کنید.")]
        [StringLength(20, ErrorMessage = "مذهب متقاضی باید حداکثر 20 کاراکتر باشد.")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "فقط از حروف  فارسی برای مذهب متقاضی استفاده کنید.")]
        public string Gilder { get; set; }

        [DisplayName("ملیت")]
        [Required(ErrorMessage = "ملیت متقاضی را وارد کنید.")]
        [StringLength(20, ErrorMessage = "ملیت متقاضی باید حداکثر 20 کاراکتر باشد.")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "فقط از حروف  فارسی برای ملیت متقاضی استفاده کنید.")]
        public string Nationality { get; set; }

        [DisplayName("شماره تلفن ثابت")]
        [Required(ErrorMessage = "شماره تلفن ثابت متقاضی را وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "پست الکترونیک متقاضی را وارد کنید.")]
        [EmailAddress(ErrorMessage = "پست الکترونیک را به شکل صحیح وارد کنید.")]
        [DisplayName("پست الکترونیک")]
        [StringLength(256, ErrorMessage = "پست الکترونیک باید حداکثر 256 کاراکتر باشد.")]
        //[Remote("IsEmailExist", nameof(Applicant), "", ErrorMessage = "این ایمیل قبلا در سیستم ثبت شده است", HttpMethod = "POST")]
        public string EmailAddress { get; set; }

        [DisplayName("شماره همراه")]
        [Required(ErrorMessage = "شماره همراه متقاضی را وارد کنید.")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}", ErrorMessage = "شماره همراه را به شکل صحیح وارد کنید.")]
        public string CellphoneNumber { get; set; }

        [DisplayName("شماره تلفن ضروری")]
        [Required(ErrorMessage = "برای مواقع ضروری یک شماره تلفن وارد کنید.")]
        public string NumberIndispensable { get; set; }

        [DisplayName("وضیعت نظام وظیفه")]
        [Required(ErrorMessage = "وضعیت نظام وظیفه متقاضی را مشخص کنید.")]
        public MilitaryStatus MilitaryStatus { get; set; }

        [DisplayName("تاریخ پایان خدمت")]
        public DateTime? ServedEndDateTime { get; set; }

        [DisplayName("نوع عضویت")]
        [Required(ErrorMessage = "نوع عضویت متقاضی را مشخص کنید.")]
        public MembershipType MembershipType { get; set; }
        
    }
}
