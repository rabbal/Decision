using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.ViewModel.Appraiser
{
    /// <summary>
    /// ویومدل درج ارزیاب - ارزیاب، مصاحبه کننده و غیره
    /// </summary>
    public class AddAppraiserViewModel
    {
        /// <summary>
        /// نام ارزیاب
        /// </summary>
        [Required(ErrorMessage = "لطفا نام  را وارد کنید")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "نام باید بین سه تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("نام")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام ارزیاب استفاده کنید")]
        public  string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی ارزیاب
        /// </summary>
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "نام خانوادگی باید بین سه تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("نام خانوادگی")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی برای نام ارزیاب استفاده کنید")]
        public  string LastName { get; set; }
        /// <summary>
        /// شماره همراه ارزیاب
        /// </summary>
        [Required(ErrorMessage = "لطفا شماره همراه را وارد کنید")]
        [DisplayName("شماره همراه")]
        public  string CellPhone { get; set; }

        /// <summary>
        /// جنسیت ارزیاب
        /// </summary>
        [DisplayName("جنسیت")]
        public  GenderType Gender { get; set; }

        /// <summary>
        /// آی دی عنوان  ارزیاب  
        /// مهندس/دکتر/...
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان را انتخاب کنید")]
        [DisplayName("عنوان")]
        public  Guid AppraiserTitleId { get; set; }

        /// <summary>
        /// لیست عنوان های ارزیاب برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> AppraiserTitles { get; set; }
    }
}