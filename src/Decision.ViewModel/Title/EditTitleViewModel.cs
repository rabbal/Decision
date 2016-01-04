using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Title
{
    public class EditTitleViewModel : BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی عنوان
        /// </summary>
        public  Guid Id { get; set; }

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
        public  TitleType Type { get; set; }
        /// <summary>
        /// گروه عنوان
        /// </summary>
        public TitleCategory Category { get; set; }
        #endregion
    }
}