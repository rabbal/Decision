using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.ArticleEvaluation;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{

    [RoutePrefix("Applicant/ArticleEvaluation")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageArticleEvaluation)]
    public partial class ArticleEvaluationController : Controller
    {
        #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IArticleService _ArticleService;
        private readonly IAppraiserService _appraiserService;
        private readonly IQuestionService _questionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleEvaluationService _evaluationService;
        #endregion

        #region	Ctor
        public ArticleEvaluationController(IReferentialApplicantService referentialApplicantService, IArticleService ArticleService, IAppraiserService appraiserService, IUnitOfWork unitOfWork, IArticleEvaluationService evaluationService, IQuestionService questionService)
        {
            _unitOfWork = unitOfWork;
            _evaluationService = evaluationService;
            _questionService = questionService;
            _appraiserService = appraiserService;
            _ArticleService = ArticleService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ArticleId}")]
       
        public virtual async Task<ActionResult> List(Guid ArticleId)
        {
            if (!_referentialApplicantService.CanManageApplicant(
                 _ArticleService.GetApplicantId(ArticleId)
                ))
                return HttpNotFound();

            var viewModel =
                await
                    _evaluationService.GetPagedList(new ArticleEvaluationSearchRequest
                    {
                        ArticleId = ArticleId,
                        PageIndex = 1
                    });
            viewModel.ArticleDetails = await _ArticleService.GetDetailes(ArticleId);
            return View(viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ArticleEvaluationSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel =
              await
                  _evaluationService.GetPagedList(request);
            if (viewModel.ArticleEvaluations == null || !viewModel.ArticleEvaluations.Any()) return Content("no-more-info");

            return PartialView(MVC.ArticleEvaluation.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [Route("Create/{ArticleId}")]
        
        
        public virtual async Task<ActionResult> Create(Guid ArticleId)
        {
            var ApplicantId = _ArticleService.GetApplicantId(ArticleId);
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = new AddArticleEvaluationViewModel
            {
                ArticleId = ArticleId,
                ApplicantId = ApplicantId,
                Questions = await _questionService.GelAllActive(),
                Evaluators = await _appraiserService.GetAsSelectedListItem(null)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("Create/{ArticleId}")]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
       
        public virtual async Task<ActionResult> Create(AddArticleEvaluationBindingModel bindingModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(bindingModel.ApplicantId)) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                this.NotyError("متأسفانه مشکلی در انجام عملیات بوجود آمده است");
                return RedirectToAction(MVC.ArticleEvaluation.Create(bindingModel.ArticleId));
            }
            await _evaluationService.Create(bindingModel);
            this.NotySuccess("علمیات با موفقیت انجام شد");
            return RedirectToAction(MVC.ArticleEvaluation.List(bindingModel.ArticleId));
        }
        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "حذف ارزیابی مقاله")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id, Guid jdugeId)
        {
            if (!_referentialApplicantService.CanManageApplicant(jdugeId)) return HttpNotFound();
            await _evaluationService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion
    }
}