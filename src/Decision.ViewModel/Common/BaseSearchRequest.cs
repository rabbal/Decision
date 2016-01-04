

using System.ComponentModel;

namespace Decision.ViewModel.Common
{
    /// <summary>
    /// کلاس پایه برای اطلاعات لازم برای جستجو و مرتب سازی
    /// </summary>
    public class BaseSearchRequest
    {
        public BaseSearchRequest()
        {
            PageSize = 10;
            PageIndex = 1;
            SortDirection = SortDirectionMode.Desc;
        }

        /// <summary>
        /// رشته جستجو
        /// </summary>
        [DisplayName("جستجو")]
        public string Term { get; set; }
        /// <summary>
        /// جهت مرتب سازی
        /// </summary>
        public string SortDirection { get; set; }
        /// <summary>
        /// تعداد کل داده ها
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// شماره صفحه
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// تعداد در صفحه
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// نام فیلد جاری برای مرتب سازی
        /// </summary>
        public string CurrentSort { get; set; }
    }
}
