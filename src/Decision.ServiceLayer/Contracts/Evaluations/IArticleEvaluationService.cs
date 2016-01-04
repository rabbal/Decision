using System;
using System.Threading.Tasks;
using Decision.ViewModel.ArticleEvaluation;

namespace Decision.ServiceLayer.Contracts.Evaluations
{
    /// <summary>
    /// نشان دهنده الزامات ارائه دهنده سرویس ارزیابی از مقاله استاد
    /// </summary
    public interface IArticleEvaluationService
    {

        /// <summary>
        /// حذف ارزیابی از مقاله استاد
        /// </summary>
        /// <param name="id">آی دی ارزیابی از مقاله استاد</param>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// درج ارزیابی از مقاله استاد جدید
        /// </summary>
        /// <param name="bindingModel"> مدل درج ارزیابی از مقاله استاد</param>
        Task Create(AddArticleEvaluationBindingModel bindingModel);

        Task<ArticleEvaluationListViewModel> GetPagedList(ArticleEvaluationSearchRequest request);
        long Count();

    }
}