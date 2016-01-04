using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.TeacherInfo;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Article;
using Decision.ViewModel.ArticleEvaluation;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.TeacherInfo
{
    /// <summary>
    /// کلاس ارائه دهنده سروسیس های لازم برای اعمال روی مقاله صادر شده توسط استاد
    /// </summary>
    public class ArticleService : IArticleService
    {
        #region Fields

        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<Article> _Articles;
        #endregion

        #region Ctor

        public ArticleService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _Articles = _unitOfWork.Set<Article>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region GetForEdit
        public Task<EditArticleViewModel> GetForEditAsync(Guid id)
        {
            return _Articles.AsNoTracking().ProjectTo<EditArticleViewModel>(_mappingEngine).FirstOrDefaultAsync(a => a.Id == id);
        }
        #endregion

        #region Delete
        public Task DeleteAsync(Guid id)
        {
            return _Articles.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region Edit
        public async Task EditAsync(EditArticleViewModel viewModel)
        {
            var Article = await _Articles.FirstAsync(a => a.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, Article);

            if (viewModel.AttachmentScan.HasValue())
                Article.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                Article.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }
            Article.LasModifierId = _userManager.GetCurrentUserId();
        }
        #endregion

        #region Create

        public async  Task<ArticleViewModel> Create(AddArticleViewModel viewModel)
        {
            var Article = _mappingEngine.Map<Article>(viewModel);
            Article.CreatorId = _userManager.GetCurrentUserId();

            if (viewModel.AttachmentScan.HasValue())
                Article.Attachment = Convert.FromBase64String(viewModel.AttachmentScan);
            else if (viewModel.AttachmentFile.HasFile())
            {
                Article.Attachment =
                    viewModel.AttachmentFile.InputStream.ConvertToByteArrary(viewModel.AttachmentFile.ContentLength);
            }

            _Articles.Add(Article);
            await _unitOfWork.SaveChangesAsync();
            return await GetArticleViewModel(Article.Id);
        }
        #endregion

        #region GetPagedList
        public async Task<ArticleListViewModel> GetPagedListAsync(ArticleSearchRequest request)
        {
            var Articles =
                _Articles.Where(a => a.TeacherId == request.TeacherId)
                    .Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .AsNoTracking()
                    .OrderByDescending(a => a.ArticleDate)
                    .AsQueryable();

            if (request.Term.HasValue())
                Articles = Articles.Where(a => a.Content.Contains(request.Term));

            var selectedArticles = Articles.ProjectTo<ArticleViewModel>(_mappingEngine);


            var query = await selectedArticles
                .Skip((request.PageIndex - 1) * 5)
                .Take(5)
                .ToListAsync();

            return new ArticleListViewModel { SearchRequest = request, Articles = query };
        }
        #endregion

        public Task<bool> IsInDb(Guid id)
        {
            return _Articles.AnyAsync(a => a.Id == id);
        }

        public Task<ArticleViewModel> GetArticleViewModel(Guid guid)
        {
            return
                _Articles.Include(a => a.Creator)
                    .Include(a => a.LasModifier)
                    .AsNoTracking()
                    .ProjectTo<ArticleViewModel>(_mappingEngine)
                    .FirstOrDefaultAsync(a => a.Id == guid);
        }


        public Task<byte[]> GetAttachment(Guid id)
        {
            return _Articles.Where(a => a.Id == id).Select(a => a.Attachment).FirstOrDefaultAsync();
        }

        public Guid GetTeacherId(Guid id)
        {
            return _Articles.Where(a => a.Id == id).Select(a => a.TeacherId).First();
        }

        public async  Task<ArticleDetails> GetDetailes(Guid id)
        {
            return
               await _Articles.Include(a => a.Teacher)
                    .Where(a => a.Id == id)
                    .ProjectTo<ArticleDetails>(_mappingEngine)
                    .FirstOrDefaultAsync();
        }

        public long Count()
        {
            return _Articles.LongCount();
        }
    }
}