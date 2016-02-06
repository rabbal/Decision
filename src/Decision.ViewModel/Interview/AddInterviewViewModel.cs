using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModel.Interview
{
    /// <summary>
    /// ویومدل درج مصاحبه
    /// </summary>
    public class AddInterviewViewModel
    {
        public AddInterviewViewModel()
        {
            Interviewers = new List<SelectListItem>();
            InterviewDate = DateTime.Now;
        }

        #region Properties
        /// <summary>
        /// تاریخ مصاحبه
        /// </summary>
        [DisplayName("تاریخ مصاحبه")]
        [Required(ErrorMessage = "لطفا تاریخ مصاحبه را مشخص کنید")]
        public  DateTime InterviewDate { get; set; }

        /// <summary>
        /// متن کامل مصاحبه
        /// </summary>
        [Required(ErrorMessage = "لطفا متن مصاحبه را وارد کنید")]
        [DisplayName("متن مصاحبه")]
        [AllowHtml]
        public  string Body { get; set; }

        /// <summary>
        /// خلاصه ای از مصاحبه
        /// </summary>
        [Required(ErrorMessage = "لطفا خلاصه مصاحبه را وارد کنید")]
       
        [DisplayName("خلاصه")]
        [AllowHtml]
        public  string Brief { get; set; }

        /// <summary>
        /// اسکن ضمیمه مصاحبه
        /// </summary>
        public  string AttachmentScan { get; set; }

        /// <summary>
        /// فایل ضمیمه مصاحبه
        /// </summary>
        [DisplayName("فایل ضمیمه")]
        public  HttpPostedFileBase AttachmentFile { get; set; }

        /// <summary>
        /// آی دی متقاضی مصاحبه شده
        /// </summary>
       [Required]
        public  Guid ApplicantId { get; set; }

        /// <summary>
        /// آی دی مصاحبه کننده ها
        /// </summary>
        [Required(ErrorMessage = "لطفا مصاحبه کننده را انتخاب کنید")]
        [DisplayName("مصاحبه کننده")]
        public  Guid InterviewerId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست مصاحبه کننده ها برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> Interviewers { get; set; }
        #endregion
    }
}