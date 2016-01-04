using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Title
{
    public class TitleViewModel : BaseViewModel
    {
        /// <summary>
        /// آی دی رکورد
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// نام عنوان
        /// </summary>
        [DisplayName("عنوان")]
        public string Name { get; set; }
        /// <summary>
        /// نوع عنوان 
        /// </summary>
        [DisplayName("نوع عنوان")]
        public  TitleType Type { get; set; }
        /// <summary>
        /// گروه عنوان
        /// </summary>
        [DisplayName("گروه عنوان")]
        public TitleCategory Category { get; set; }
    }
}