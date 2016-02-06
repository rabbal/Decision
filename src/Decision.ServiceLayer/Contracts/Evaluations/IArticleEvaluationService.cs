using System;
using System.Threading.Tasks;
using Decision.ViewModel.ArticleEvaluation;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارزیابی از مقاله متقاضی
    /// </summary
    public interface IArticleEvaluationService
    {

        /// <summary>
        /// حذف ارزیابی از مقاله متقاضی
        /// </summary>
        /// <param name="id">آی دی ارزیابی از مقاله متقاضی</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// درج ارزیابی از مقاله متقاضی جدید
        /// </summary>
        /// <param name="bindingModel"> مدل درج ارزیابی از مقاله متقاضی</param>
        Task Create(AddArticleEvaluationBindingModel bindingModel);

        Task<ArticleEvaluationListViewModel> GetPagedList(ArticleEvaluationSearchRequest request);
        long Count();

    }
}