using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.ApplicantInfo;

// ReSharper disable InconsistentNaming

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// ویومدل درج سوابق متقاضی
    /// </summary>
    public class AddEducationalBackgroundViewModel
    {
        public AddEducationalBackgroundViewModel()
        {
            EntryDate = GraduationDate = DateTime.Now;
        }

        #region Properties

        /// <summary>
        /// مقطع تحصیلی دانشگاهی
        /// </summary>
        [DisplayName("مقطع تحصیلات دانشگاهی")]
        public AcademicDegrees AcademicDegree { get; set; }

        /// <summary>
        /// موضوع پایان نامه مدرک
        /// </summary>
        [Required(ErrorMessage = "لفطا موضوع پایان نامه را وارد کنید")]
        [DisplayName("موضوع پایان نامه")]
        public string ThesisTopic { get; set; }

        /// <summary>
        /// زمان فارغ التحصیل شدن در این مدرک
        /// </summary>
        [DisplayName("تاریخ فارغ التحصیلی")]
        [Required(ErrorMessage = "لفطا تاریخ فارغ التحصیل شدن در این مدرک را وارد کنید")]
        public DateTime GraduationDate { get; set; }

        /// <summary>
        /// تاریخ ورود به مقطع تحصیلی
        /// </summary>
        [DisplayName("تاریخ شروع مقطع")]
        [Required(ErrorMessage = "لفطا تاریخ  ورود به مقطع تحصیلی را وارد کنید")]
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// متقاضی مشاور
        /// </summary>

        [StringLength(50, ErrorMessage = "نام متقاضی مشاور باید بین دو تا 50 کاراکتر باشد")]
        [DisplayName("متقاضی مشاور")]
        public string Advisor { get; set; }

        /// <summary>
        /// متقاضی راهنما
        /// </summary>

        [StringLength(50, ErrorMessage = "نام متقاضی راهنما باید بین دو تا 50 کاراکتر باشد")]
        [DisplayName("متقاضی راهنما")]
        public string Supervisor { get; set; }

        /// <summary>
        ///توضیحاتی از قبیل نام سایر اساتید مشاور و داوران
        /// </summary>

        [DisplayName("توضیحات")]
        public string Description { get; set; }
        /// <summary>
        /// معدل کل مقطع
        /// </summary>
        [DisplayName("معدل کل")]
        [Range(0.00, 20.00, ErrorMessage = "نمره معدل کل میبایست بین صفر تا ۲۰ باشد")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا معدل کل را به شکل صحیح وارد کنید")]
        public decimal GPA { get; set; }
        /// <summary>
        /// نمره پایان نامه
        /// </summary>
        [DisplayName("نمره پایان نامه")]
        [Range(0.00, 20.00, ErrorMessage = "نمره پایان نامه میبایست بین صفر تا ۲۰ باشد")]
        [RegularExpression(@"\d+(\.\d{2})?", ErrorMessage = "لطفا نمره پایان نامه را به شکل صحیح وارد کنید")]
        public decimal ThesisScore { get; set; }
        /// <summary>
        /// اسکن فایل ضمیمه مدرک تحصیلی
        /// </summary>
        public string AttachmentScan { get; set; }
        /// <summary>
        /// اسکن فایل ضمیمه مدرک تحصیلی
        /// </summary>
        [DisplayName("فایل ضمیمه مدرک")]
        public HttpPostedFileBase AttachmentFile { get; set; }
        /// <summary>
        /// آی دی متقاضی صاحب مدرک
        /// </summary>
        [DisplayName("متقاضی")]
        [Required]
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// امتیاز 
        /// </summary>
        [DisplayName("امتیاز")]
        [Required(ErrorMessage = "لطفا برای این مدرک امتیاز دهی کنید")]
        public double Score { get; set; }
        /// <summary>
        /// کشور محل تحصیل
        /// </summary>
        [DisplayName("کشور")]
        public string Country { get; set; }
        /// <summary>
        /// دانشگاه
        /// </summary>
        [DisplayName("دانشگاه")]
        [Required(ErrorMessage = "لطفا دانشگاه محل تحصیل را مشخص کنید")]
        public string University { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        [DisplayName("رشته دانشگاهی")]
        [Required(ErrorMessage = "لطفا رشته تحصیلی را مشخص کنید")]
        public string Field { get; set; }
        #endregion


    }
}