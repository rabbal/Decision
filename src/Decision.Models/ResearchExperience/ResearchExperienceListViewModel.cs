using System.Collections.Generic;

namespace Decision.ViewModel.ResearchExperience
{
    /// <summary>
    /// ویومدل نمایش لیست سابقه های پژوهشی
    /// </summary>
    public class ResearchExperienceListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public ResearchExperienceSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش سابقه های پژوهشی
        /// </summary>
        public IEnumerable<ResearchExperienceViewModel> ResearchExperiences { get; set; }
    }
}