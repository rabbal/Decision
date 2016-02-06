using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.Common.Helpers.Extentions;
using Decision.Common.Helpers.Json;
using Decision.Common.HtmlCleaner;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Article;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/Article")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageArticle)]
    public partial class ArticleController : Controller
    {
	    #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleService _ArticleService;
        #endregion

        #region	Ctor
        public ArticleController(IUnitOfWork unitOfWork, IArticleService ArticleService,IReferentialApplicantService referentialApplicantService)
        {
            _unitOfWork = unitOfWork;
            _ArticleService = ArticleService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست مقالات متقاضی", PreservedRouteParameters = "ApplicantId",Key = "Article_List")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = await _ArticleService.GetPagedListAsync(new ArticleSearchRequest
            {
                ApplicantId =  ApplicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]

        public virtual async Task<ActionResult> ListAjax(ArticleSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel = await _ArticleService.GetPagedListAsync(request);
            if (viewModel.Articles == null || !viewModel.Articles.Any()) return Content("no-more-info");
            return PartialView(MVC.Article.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual ActionResult Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = new AddArticleViewModel
            {
                ApplicantId = ApplicantId
            };
            return PartialView(MVC.Article.Views._Create,viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج مقاله")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        
        public virtual async Task<ActionResult> Create(AddArticleViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
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
           var newArticle=await  _ArticleService.Create(viewModel);

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
            var viewModel = await _ArticleService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        [Audit(Description = "ویرایش مقاله ")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        
        public virtual async Task<ActionResult> Edit(EditArticleViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!await _ArticleService.IsInDb(viewModel.Id))
                this.AddErrors("Content", "مقاله مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
                return View(viewModel);

            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Content = viewModel.Content.ToSafeHtml();
            await _ArticleService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("Content", string.Format(message, "مقاله "));

            if (!ModelState.IsValid)
                return View(viewModel);
            return RedirectToAction(MVC.Article.List(viewModel.ApplicantId));
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "حذف مقاله ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        
        public virtual async Task<ActionResult> Delete(Guid? id,Guid ApplicantId)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            await _ArticleService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion
        
        #region DownloadDocument
        [Route("GetFile/{id}/Applicant/{ApplicantId}")]
        [ApplicantAuthorize]
        public virtual async Task<ActionResult> GetDocument(Guid id,Guid ApplicantId)
        {
            var data = await _ArticleService.GetAttachment(id);
            return File(data, "application/pdf", $"{id}.{"pdf"}");
        }
        #endregion
    }
}
