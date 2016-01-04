using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCenter
{
    public class EditTrainingCenterViewModel : BaseRowVersion
    {
        /// <summary>
        /// آی دی مرکز کارآموزی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// نام مرکز کار آموزی
        /// </summary>
        [Required(ErrorMessage = "لطفا نام مرکز کارآموزی را وارد کنید")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "نام مرکز کارآموزی باید بین دو تا ۲۵۶ کاراکتر باشد")]
        
        [DisplayName("نام")]
        public  string CenterName { get; set; }
        /// <summary>
        /// نشانی
        /// </summary>
        
        [DisplayName("نشانی")]
        
        public  string Location { get; set; }
        /// <summary>
        /// شماره تلفن 1
        /// </summary>
        [DisplayName("شماره تلفن ۱")]
        [StringLength(20, ErrorMessage = "شماره تلفن باید حداکثر ۲۰ کاراکتر باشد")]
       
        public  string PhoneNumber1 { get; set; }
        /// <summary>
        /// شماره تلفن 2
        /// </summary>
        [DisplayName("شماره تلفن ۲")]
        [StringLength(20, ErrorMessage = "شماره تلفن باید حداکثر ۲۰ کاراکتر باشد")]
        public  string PhoneNumber2 { get; set; }
        /// <summary>
        /// توضیحات در صورت نیاز
        /// </summary>
        [DisplayName("توضیحات")]
        [StringLength(1024, ErrorMessage = "نام مرکز کارآموزی باید حداکثر ۱۰۲۴ کاراکتر باشد")]
        public  string Description { get; set; }
        /// <summary>
        /// آی دی شهر
        /// </summary>
        [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا شهر را مشخص کنید")]
        public string City { get; set; }
        /// <summary>
        /// آی دی استان
        /// </summary>
        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان را مشخص کنید")]
        public string State { get; set; }
        /// <summary>
        /// لیست استان ها برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> States { get; set; }
        /// <summary>
        /// لیست شهر ها برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}