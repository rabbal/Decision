using System.Collections.Generic;

namespace Decision.ViewModel.WorkExperience
{
    /// <summary>
    /// ویومدل نمایش لیست سابقه های کاری
    /// </summary>
    public class WorkExperienceListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public WorkExperienceSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش سابقه کاری
        /// </summary>
        public IEnumerable<WorkExperienceViewModel> WorkExperiences { get; set; }

    }
}