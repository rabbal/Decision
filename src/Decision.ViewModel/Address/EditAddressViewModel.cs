using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Address
{
    public class EditAddressViewModel:BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی آدرس
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        [StringLength(20, ErrorMessage = "شماره همراه باید حداکثر ۲۰ کاراکتر باشد")]
        [DisplayName("شماره همراه")]
        [RegularExpression("۰۹(۱[۰-۹]|۳[۱-۹]|۲[۱-۹])-?[۰-۹]{3}-?[۰-۹]{4}", ErrorMessage = "لطفا شماره همراه را به شکل صحیح وارد کنید")]
        public string CellPhone { get; set; }

        /// <summary>
        /// نشانی
        /// </summary>
        [Required(ErrorMessage = "لطفا نشانی را وارد کنید")]
        [DisplayName("نشانی")]
        public string Location { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        [DisplayName("تلفن ثابت")]
        [StringLength(20, ErrorMessage = "شماره تلفن باید حداکثر ۲۰ کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// نوع آدرس
        /// </summary>
        [DisplayName("نوع آدرس")]
        [Required(ErrorMessage = "لطفا نوع آدرس را انتخاب کنید")]
        public AddressType Type { get; set; }
        /// <summary>
        /// آی دی متقاضی
        /// </summary>
        [Required]
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا شهر را مشخص کنید")]
        public string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان را مشخص کنید")]
        public string State { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        #endregion
    }
}