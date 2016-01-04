using System.Collections.Generic;

namespace Decision.ViewModel.Institution
{
    /// <summary>
    /// ویو مدل نمایش لیست موسسه های آموزشی
    /// </summary>
    public class InstitutionListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public InstitutionSearchRequest Request { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش موسسه آموزشی
        /// </summary>
        public IEnumerable<InstitutionViewModel> Institutions { get; set; }
    }
}