using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModel.ReferentialApplicant
{
    /// <summary>
    /// ویومدل درج ارجاع متقاضی
    /// </summary>
    public class AddReferentialApplicantViewModel
    {
        #region Properties
      
        /// <summary>
        /// آی  دی کاربری که این متقاضی برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        [Required(ErrorMessage = "لطفا اپراتور را انتخاب کنید")]
        [DisplayName("اپراتور")]
        public  Guid ReferencedToId { get; set; }
        /// <summary>
        ///  آی دی متقاضی ارجاع داده شده
        /// </summary>
        [Required()]
        public  Guid ApplicantId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست کاربران برای انتخاب ارجاع دادن متقاضی به آنها، در لیست آبشاری
        /// </summary>
        public  IEnumerable<SelectListItem> RefrencedToUsers { get; set; }
        #endregion
    }
}