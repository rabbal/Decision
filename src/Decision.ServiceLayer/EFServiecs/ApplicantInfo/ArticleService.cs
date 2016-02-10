using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Article;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.ApplicantInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی مقاله صادر شده توسط متقاضی
    /// </summary>
    public class ArticleService : IArticleService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Article> _articles;
        #endregion

        #region Ctor

        public ArticleService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _articles = _unitOfWork.Set<Article>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditArticleViewModel> GetForEditAsync(Guid id)
        {
            return _articles.AsNoTracking().ProjectTo<EditArticleViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _articles.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditArticleViewModel viewModel)
        {
            var article = await _articles.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, article);

            if (viewModel.AttachmentScan.HasValue())
                article.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                article.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }
        }
        #endregion

        #region Create

        public async Task<ArticleViewModel> Create(AddArticleViewModel viewModel)
        {
            var article = _mappingEngine.Map<Article>(viewModel);

            if (viewModel.AttachmentScan.HasValue())
                article.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                article.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }

            _articles.Add(article);
            await _unitOfWork.SaveChangesAsync();
            return await GetArticleViewModel(article.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<ArticleListViewModel> GetPagedListAsync(ArticleSearchRequest request)
        {
            var articles =
                _articles.Where(a => a.ApplicantId == request.ApplicantId)
                    .Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .AsNoTracking()
                    .OrderByDescending(a => a.PublicatedOn)
                    .AsQueryable();

            var selectedArticles = articles.ProjectTo<ArticleViewModel>(_mappingEngine);
            var resultsToSkip = (request.PageIndex - 1)*5;
            var query = await selectedArticles
                .Skip(() => resultsToSkip)
                .Take(5)
                .ToListAsync();

            return new ArticleListViewModel { SearchRequest = request, Articles = query };
        }
        #endregion

        public Task<bool> IsInDb(Guid id)
        {
            return _articles.AnyAsync(a => a.Id == id);
        }

        public Task<ArticleViewModel> GetArticleViewModel(Guid guid)
        {
            return
                _articles.Include(a => a.CreatedBy)
                    .Include(a => a.ModifiedBy)
                    .AsNoTracking()
                    .ProjectTo<ArticleViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }


        public Task<byte[]> GetAttachment(Guid id)
        {
            return _articles.Where(a => a.Id == id).Select(a => a.Attachment).FirstOrDefaultAsync();
        }

        public Guid GetApplicantId(Guid id)
        {
            return _articles.Where(a => a.Id == id).Select(a => a.ApplicantId).First();
        }


        public long Count()
        {
            return _articles.LongCount();
        }
    }
}