using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModel.ReferentialTeacher
{
    /// <summary>
    /// ویومدل درج ارجاع استاد
    /// </summary>
    public class AddReferentialTeacherViewModel
    {
        #region Properties
      
        /// <summary>
        /// آی  دی کاربری که این استاد برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        [Required(ErrorMessage = "لطفا اپراتور را انتخاب کنید")]
        [DisplayName("اپراتور")]
        public  Guid ReferencedToId { get; set; }
        /// <summary>
        ///  آی دی استاد ارجاع داده شده
        /// </summary>
        [Required()]
        public  Guid TeacherId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست کاربران برای انتخاب ارجاع دادن استاد به آنها، در لیست آبشاری
        /// </summary>
        public  IEnumerable<SelectListItem> RefrencedToUsers { get; set; }
        #endregion
    }
}