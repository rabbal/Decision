using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Role
{
    public class EditRoleViewModel : BaseRowVersion
    {

        /// <summary>
        /// آی دی 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// نام گروه کاربری
        /// </summary>
        [Required(ErrorMessage = "لطفا نام گروه را وارد کنید")]
        [DisplayName("نام گروه")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر های نام گروه نباید کمتر از ۵ و بیشتر از ۵۰ باشد", MinimumLength = 5)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی استفاده کنید")]
        
        public string Name { get; set; }
        /// <summary>
        /// کاربران این گروه قفل شوند؟
        /// </summary>
        [DisplayName("قفل شوند")]
        public bool IsBanned { get; set; }
        /// <summary>
        /// دسترسی های گروه کاربری
        /// </summary>
        public string[] PermissionNames { get; set; }
        /// <summary>
        /// لیست دسترسی ها به صورت لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> Permissions { get; set; }
        /// <summary>
        /// آیا گروه سیستمی است؟
        /// </summary>
        public bool IsSystemRole { get; set; }
    }
}
