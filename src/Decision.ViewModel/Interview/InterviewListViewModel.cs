using System.Collections.Generic;

namespace Decision.ViewModel.Interview
{
    /// <summary>
    /// ویو مدل نمایش لیست مصاحبه ها
    /// </summary>
    public class InterviewListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public InterviewSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش مصاحبه
        /// </summary>
        public IEnumerable<InterviewViewModel> Interviews { get; set; }
    }
}