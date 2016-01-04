using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.Common;

namespace Decision.ViewModel.Title
{
    public class AddTitleViewModel
    {
        #region Properties
        /// <summary>
        /// نام عنوان
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان را وارد کنید")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "عنوان باید بین دو تا ۲۵۶ کاراکتر باشد")]
        [DisplayName("عنوان")]
        public  string Name { get; set; }
        /// <summary>
        /// نوع عنوان 
        /// </summary>
        [DisplayName("نوع")]
        [Required(ErrorMessage = "لطفا نوع عنوان را وارد کنید")]
        public  TitleType Type { get; set; }
        /// <summary>
        /// گروه عنوان
        /// </summary>
        [DisplayName("گروه")]
        public TitleCategory Category { get; set; }
        /// <summary>
        /// آیا گروه هم قابل انتخاب باشد؟
        /// 
        /// </summary>
        public bool CategoryIsHidden { get; set; }
        #endregion 
    }
}