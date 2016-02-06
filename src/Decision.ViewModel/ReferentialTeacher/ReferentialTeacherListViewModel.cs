using System.Collections.Generic;

namespace Decision.ViewModel.ReferentialApplicant
{
    /// <summary>
    /// ویو مدل نمایش لیست ارجاعات متقاضی
    /// </summary>
    public class ReferentialApplicantListViewModel
    {
        /// <summary>
        /// لیست ویو مدل نمایش ارجاعات متقاضی
        /// </summary>
        public IEnumerable<ReferentialApplicantViewModel> ReferentialApplicants { get; set; }
    }
}