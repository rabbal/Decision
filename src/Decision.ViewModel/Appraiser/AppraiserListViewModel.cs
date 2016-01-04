using System.Collections.Generic;

namespace Decision.ViewModel.Appraiser
{
    /// <summary>
    /// ویو مدل نمایش لیست ارزیاب ها
    /// </summary>
    public class AppraiserListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public AppraiserSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش ارزیاب
        /// </summary>
        public IEnumerable<AppraiserViewModel> Appraisers { get; set; }
    }
}