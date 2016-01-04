using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی سوابق تحصیلی استاد
    /// </summary>
    public class EducationalBackgroundSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی استاد مربوط به سابقه تحصیلی
        /// </summary>
        public Guid TeacherId { get; set; }
    }
}