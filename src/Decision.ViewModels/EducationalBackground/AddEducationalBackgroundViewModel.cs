using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Decision.DomainClasses.Applicants;

// ReSharper disable InconsistentNaming

namespace Decision.ViewModels.EducationalBackground
{
    public class AddEducationalBackgroundViewModel
    {
        public AddEducationalBackgroundViewModel()
        {
            EntryDate = GraduationDate = DateTime.Now;
        }

        #region Properties

        [DisplayName("مقطع تحصیلات دانشگاهی")]
        public AcademicDegrees AcademicDegree { get; set; }

        [Required(ErrorMessage = "لفطا موضوع پایان نامه را وارد کنید")]
        [DisplayName("موضوع پایان نامه")]
        public string ThesisTopic { get; set; }

        [DisplayName("تاریخ فارغ التحصیلی")]
        [Required(ErrorMessage = "لفطا تاریخ فارغ التحصیل شدن در این مدرک را وارد کنید")]
        public DateTime GraduationDate { get; set; }

        [DisplayName("تاریخ شروع مقطع")]
        [Required(ErrorMessage = "لفطا تاریخ  ورود به مقطع تحصیلی را وارد کنید")]
        public DateTime EntryDate { get; set; }


        [StringLength(50, ErrorMessage = "نام متقاضی مشاور باید بین دو تا 50 کاراکتر باشد")]
        [DisplayName("متقاضی مشاور")]
        public string Advisor { get; set; }


        [StringLength(50, ErrorMessage = "نام متقاضی راهنما باید بین دو تا 50 کاراکتر باشد")]
        [DisplayName("متقاضی راهنما")]
        public string Supervisor { get; set; }


        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("معدل کل")]
        [Range(0.00, 20.00, ErrorMessage = "نمره معدل کل میبایست بین صفر تا ۲۰ باشد")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا معدل کل را به شکل صحیح وارد کنید")]
        public decimal GPA { get; set; }
        [DisplayName("نمره پایان نامه")]
        [Range(0.00, 20.00, ErrorMessage = "نمره پایان نامه میبایست بین صفر تا ۲۰ باشد")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا نمره پایان نامه را به شکل صحیح وارد کنید")]
        public decimal ThesisScore { get; set; }
        public string AttachmentScan { get; set; }
        [DisplayName("فایل ضمیمه مدرک")]
        public HttpPostedFileBase AttachmentFile { get; set; }
        [DisplayName("متقاضی")]
        [Required]
        public Guid ApplicantId { get; set; }
        [DisplayName("امتیاز")]
        [Required(ErrorMessage = "لطفا برای این مدرک امتیاز دهی کنید")]
        public double Score { get; set; }
        [DisplayName("کشور")]
        public string Country { get; set; }
        [DisplayName("دانشگاه")]
        [Required(ErrorMessage = "لطفا دانشگاه محل تحصیل را مشخص کنید")]
        public string University { get; set; }
        [DisplayName("رشته دانشگاهی")]
        [Required(ErrorMessage = "لطفا رشته تحصیلی را مشخص کنید")]
        public string Field { get; set; }
        #endregion


    }
}