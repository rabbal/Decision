using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Interview
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی مصاحبه ها
    /// </summary>
    public class InterviewSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی مربوطه
        /// </summary>
        public Guid ApplicantId { get; set; }
    }
}