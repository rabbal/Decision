using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalBackground
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی سوابق تحصیلی متقاضی
    /// </summary>
    public class EducationalBackgroundSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوط به سابقه تحصیلی
        /// </summary>
        public Guid ApplicantId { get; set; }
    }
}