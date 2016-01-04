using System.ComponentModel;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Title
{
    /// <summary>
    /// کلاسی برای کپسوله کردن اطلاعات فیلترینگ
    /// </summary>
    public class TitleSearchRequest : BaseSearchRequest
    {
        ///<summary>
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
