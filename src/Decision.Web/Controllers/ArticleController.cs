using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.Common.HtmlCleaner;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Article;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;
using Decision.Common.Extentions;
namespace Decision.Web.Controllers
{

    [RoutePrefix("Applicant/Article")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageArticle)]
    public partial class ArticleController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleService _articleService;
        #endregion

        #region	Ctor
        public ArticleController(IUnitOfWork unitOfWork, IArticleService articleService)
        {
            _unitOfWork = unitOfWork;
            _articleService = articleService;

        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست مقالات متقاضی", PreservedRouteParameters = "ApplicantId", Key = "Article_List")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _articleService.GetPagedListAsync(new ArticleSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]

        public virtual async Task<ActionResult> ListAjax(ArticleSearchRequest request)
        {
            var viewModel = await _articleService.GetPagedListAsync(request);
            if (viewModel.Articles == null || !viewModel.Articles.Any()) return Content("no-more-info");
            return PartialView(MVC.Article.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual ActionResult Create(Guid applicantId)
        {
            var viewModel = new AddArticleViewModel
            {
                ApplicantId = applicantId
            };
            return PartialView(MVC.Article.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج مقاله")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]

        public virtual async Task<ActionResult> Create(AddArticleViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Article.Views._Create, viewModel)
                    }
                };
            }
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Content = viewModel.Content.ToSafeHtml();
            var newArticle = await _articleService.Create(viewModel);

            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Article.Views._ArticleItem, newArticle)
                }
            };
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id}")]

        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _articleService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();

            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        [Activity(Description = "ویرایش مقاله ")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]

        public virtual async Task<ActionResult> Edit(EditArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Content = viewModel.Content.ToSafeHtml();
            await _articleService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync();
            return RedirectToAction(MVC.Article.List(viewModel.ApplicantId));
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "حذف مقاله ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]

        public virtual async Task<ActionResult> Delete(Guid? id, Guid applicantId)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            await _articleService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region DownloadDocument
        [Route("GetFile/{id}/Applicant/{ApplicantId}")]
        public virtual async Task<ActionResult> GetDocument(Guid id, Guid applicantId)
        {
            var data = await _articleService.GetAttachment(id);
            return File(data, "application/pdf", $"{id}.{"pdf"}");
        }
        #endregion
    }
}
