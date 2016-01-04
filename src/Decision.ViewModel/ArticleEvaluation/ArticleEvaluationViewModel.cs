using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ArticleEvaluation
{
    /// <summary>
    /// ویومدل نمایش ارزیابی از مقاله استاد
    /// </summary>
    public class ArticleEvaluationViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی ارزیابی از رأی
        /// </summary>
        public  Guid Id { get; set; }
        [DisplayName("امتیاز")]
        public decimal Score { get; set; }
        /// <summary>
        /// نام ارزیاب مقاله صادر شده
        /// </summary>
        [DisplayName("نام ارزیاب")]
        public  string EvaluatorName { get; set; }
        #endregion

    }
}