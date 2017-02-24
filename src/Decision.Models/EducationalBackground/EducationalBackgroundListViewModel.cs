using System.Collections.Generic;

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// ویو مدل نمایش لیست سوابق تحصیلی متقاضی
    /// </summary>
    public class EducationalBackgroundListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public EducationalBackgroundSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش سابقه تحصیلی متقاضی
        /// </summary>
        public IEnumerable<EducationalBackgroundViewModel> EducationalBackgrounds { get; set; }

    }
}