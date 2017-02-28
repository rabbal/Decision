using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModels.ApplicantEvaluation.Interview
{
    public class EditInterviewViewModel 
    {
   
        #region Properties
        public Guid Id { get; set; }
        [DisplayName("تاریخ مصاحبه")]
        public DateTime InterviewDate { get; set; }

        [Required(ErrorMessage = "لطفا متن مصاحبه را وارد کنید")]
        [DisplayName("متن مصاحبه")]
        [AllowHtml]
        public string Body { get; set; }

        [Required(ErrorMessage = "لطفا خلاصه مصاحبه را وارد کنید")]

        [DisplayName("خلاصه")]
        [AllowHtml]
        public string Brief { get; set; }

        public string AttachmentScan { get; set; }

        [DisplayName("فایل ضمیمه")]
        public HttpPostedFileBase AttachmentFile { get; set; }

       [Required]
        public Guid ApplicantId { get; set; }

        [Required(ErrorMessage = "لطفا مصاحبه کننده را انتخاب کنید")]
        [DisplayName("مصاحبه کننده")]
        public Guid InterviewerId { get; set; }
        #endregion

        #region SelectListItems
        public IEnumerable<SelectListItem> Interviewers { get; set; }
        #endregion
    }
}