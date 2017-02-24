using System;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EntireEvaluation
{
    /// <summary>
    /// کلاسی برای کپسوله سازی اطلاعات جستجو و مرتب سازی ارزیابی های انجام شده از متقاضی ها
    /// </summary>
    public class EntireEvaluationSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی متقاضی ارزیابی شده
        /// </summary>
        public  Guid  ApplicantId { get; set; }

    }
}