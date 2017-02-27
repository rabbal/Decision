using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModels.ApplicantEvaluation.EntireEvaluation
{
    public class EditEntireEvaluationViewModel : BaseRowVersion
    {
        #region Properties
        public  Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا متن ارزیابی را وارد کنید")]
        [DisplayName("متن ارزیابی")]
        [AllowHtml]
        public  string Content { get; set; }

        [DisplayName("تاریخ")]
        [Required(ErrorMessage = "لطفا تاریخ ارزیابی را مشخص کنید")]
        public  DateTime EvaluationDate { get; set; }

        [Required(ErrorMessage = "لطفا خلاصه ارزیابی را وارد کنید")]
        [AllowHtml]
        [DisplayName("خلاصه ارزیابی")]
        public  string Brief { get; set; }

        [Required(ErrorMessage = "لطفا نقاط ضعف متقاضی را وارد کنید")]
        [AllowHtml]
        [DisplayName("نقاط ضعف")]
        public  string Foible { get; set; }

        [Required(ErrorMessage = "لطفا نقاط قوت متقاضی را وارد کنید")]
        [DisplayName("نقاط قوت")]
        [AllowHtml]
        public  string StrongPoint { get; set; }

        public  string AttachmentScan { get; set; }

        [DisplayName("فایل ضمیمه")]
        public  HttpPostedFileBase AttachmentFile { get; set; }

        [Required]
        public  Guid ApplicantId { get; set; }

        [Required(ErrorMessage = "لطفا ارزیاب را انتخاب کنید")]
        [DisplayName("ارزیاب")]
        public  Guid EvaluatorId { get; set; }
        #endregion

        #region SelectListIten
        public  IEnumerable<SelectListItem> Evaluators { get; set; }
        #endregion
    }
}