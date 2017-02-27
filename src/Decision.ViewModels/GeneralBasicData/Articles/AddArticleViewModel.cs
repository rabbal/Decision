using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModels.GeneralBasicData.Article
{
    public class AddArticleViewModel
    {
        public AddArticleViewModel()
        {
            ArticleDate = DateTime.Now;
        }
        #region Properties
        [DisplayName("کد مقاله")]
        [Required(ErrorMessage = "لطفا کد مقاله را وارد کنید")]
        [StringLength(50,ErrorMessage = "کد مقاله نباید بیشتر از ۵۰ کاراکتر باشد")]
        public string Code { get; set; }
        [Required(ErrorMessage = "لطفا محتوای مقاله را وارد کنید")]
        [DisplayName("محتوای مقاله")]
        [AllowHtml]
        public  string Content { get; set; }

        
        [DisplayName("خلاصه مقاله")]
        [AllowHtml]
        public string Brief { get; set; }

        [DisplayName("تاریخ ارائه")]
        [Required(ErrorMessage = "لفطا تاریخ ارائه مقاله را وارد کنید")]
        public  DateTime ArticleDate { get; set; }

        public  string AttachmentScan { get; set; }


        [DisplayName("فایل ضمیمه")]
        public  HttpPostedFileBase AttachmentFile { get; set; }

        [DisplayName("متقاضی")]
        [Required]
        public  Guid ApplicantId { get; set; }
        #endregion
    }
}