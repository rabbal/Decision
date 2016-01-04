using System.Collections.Generic;

namespace Decision.ViewModel.Title
{
    /// <summary>
    /// ویو مدل نمایش لیست عنوان ها
    /// </summary>
    public class TitleListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو
        /// </summary>
        public TitleSearchRequest Request { get; set; }
        /// <summary>
        /// لیست نمایشی برای عنوان ها
        /// </summary>
        public IEnumerable<TitleViewModel> Titles { get; set; }
    }
}
