using System.Collections.Generic;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویو مدل نمایش لیست سابقه های آموزشی
    /// </summary>
    public class EducationalExperienceListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public EducationalExperienceSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش سابقه آموزشی
        /// </summary>
        public IEnumerable<EducationalExperienceViewModel> EducationalExperiences { get; set; }

    }
}