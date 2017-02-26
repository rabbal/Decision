using System;
using System.Threading.Tasks;
using Decision.ViewModels.Article;

namespace Decision.ServiceLayer.Interfaces.Applicants
{
    public interface IArticleService
    {
        Task<EditArticleViewModel> GetForEditAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task EditAsync(EditArticleViewModel viewModel);
        Task<ArticleViewModel> Create(AddArticleViewModel viewModel);
        Task<ArticleListViewModel> GetPagedListAsync(ArticleSearchRequest request);
        Task<bool> IsInDb(Guid id);
        Task<ArticleViewModel> GetArticleViewModel(Guid guid);
        Task<byte[]> GetAttachment(Guid id);
        Guid GetApplicantId(Guid id);
        long Count();
    }
}