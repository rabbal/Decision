using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModel.Role
{
    public class AddRoleViewModel
    {
        /// <summary>
        /// نام گروه کاربری
        /// </summary>
        [Required(ErrorMessage = "لطفا نام گروه را وارد کنید")]
        [DisplayName("نام گروه")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر های نام گروه نباید کمتر از ۵ و بیشتر از ۵۰ باشد", MinimumLength = 5)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF,۰-۹\s]*$", ErrorMessage = "لطفا فقط ازاعداد و حروف  فارسی استفاده کنید")]
        public string Name { get; set; }
    }
}
