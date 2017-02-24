using System.Collections.Generic;

namespace Decision.ViewModel.EntireEvaluation
{
    /// <summary>
    /// ویو مدل نمایش لیست ارزیابی از متقاضی ها
    /// </summary>
    public class EntireEvaluationListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public EntireEvaluationSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش ارزیابی از متقاضی
        /// </summary>
        public IEnumerable<EntireEvaluationViewModel> EntireEvaluations { get; set; }
    }
}