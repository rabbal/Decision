using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Article
{
    /// <summary>
    /// ویومدل ویرایش مقاله داده شده توسط متقاضی
    /// </summary>
    public class EditArticleViewModel : BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی مقاله داده شده توسط متقاضی
        /// </summary>
        public  Guid Id { get; set; }

        [DisplayName("کد مقاله")]
        [Required(ErrorMessage = "لطفا کد مقاله را وارد کنید")]
        [StringLength(50, ErrorMessage = "کد مقاله نباید بیشتر از ۵۰ کاراکتر باشد")]
        public string Code { get; set; }
        /// <summary>
        /// محتوای مقاله 
        /// </summary>
        [Required(ErrorMessage = "لطفا محتوای مقاله را وارد کنید")]
        [DisplayName("محتوای مقاله")]
        [AllowHtml]
        public  string Content { get; set; }

        /// <summary>
        /// خلاصه مقاله 
        /// </summary>
        
        [DisplayName("خلاصه مقاله")]
        [AllowHtml]
        public  string Brief { get; set; }

        /// <summary>
        /// تاریخ ارائه مقاله
        /// </summary>
        [DisplayName("تاریخ ارائه")]
        [Required(ErrorMessage = "لفطا تاریخ ارائه مقاله را وارد کنید")]
        public  DateTime ArticleDate { get; set; }

        /// <summary>
        /// اسکن فایل ضمیمه مقاله صدور شده
        /// </summary>
        public  string AttachmentScan { get; set; }


        /// <summary>
        /// فایل ضمیمه مقاله صدور شده
        /// </summary>
        [DisplayName("فایل ضمیمه")]
        public  HttpPostedFileBase AttachmentFile { get; set; }

        /// <summary>
        /// آی دی متقاضی صدرو کننده مقاله
        /// </summary>
        [DisplayName("متقاضی")]
        [Required]
        public  Guid ApplicantId { get; set; }
        #endregion
    }
}