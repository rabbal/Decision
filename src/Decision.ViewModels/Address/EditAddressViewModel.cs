using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.Address
{
    public class EditAddressViewModel:BaseRowVersion
    {
        #region Properties
        public long Id { get; set; }

        [StringLength(20, ErrorMessage = "شماره همراه باید حداکثر ۲۰ کاراکتر باشد")]
        [DisplayName("شماره همراه")]
        [RegularExpression("۰۹(۱[۰-۹]|۳[۱-۹]|۲[۱-۹])-?[۰-۹]{3}-?[۰-۹]{4}", ErrorMessage = "لطفا شماره همراه را به شکل صحیح وارد کنید")]
        public string CellPhone { get; set; }
        
        [Required(ErrorMessage = "لطفا نشانی را وارد کنید")]
        [DisplayName("نشانی")]
        public string Location { get; set; }
        
        [DisplayName("تلفن ثابت")]
        [StringLength(20, ErrorMessage = "شماره تلفن باید حداکثر ۲۰ کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        
        [DisplayName("نوع آدرس")]
        [Required(ErrorMessage = "لطفا نوع آدرس را انتخاب کنید")]
        public AddressType Type { get; set; }

        [Required]
        public Guid ApplicantId { get; set; }

        [DisplayName("شهر")]
        [Required(ErrorMessage = "لطفا شهر را مشخص کنید")]
        public string City { get; set; }

        [DisplayName("استان")]
        [Required(ErrorMessage = "لطفا استان را مشخص کنید")]
        public string State { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }

        #endregion
    }
}