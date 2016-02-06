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
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.EntireEvaluation;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/EntireEvaluation")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageEntireEvaluation)]
    public partial class EntireEvaluationController : Controller
    {
        #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntireEvaluationService _entireEvaluationService;
        #endregion

        #region	Ctor
        public EntireEvaluationController(IUnitOfWork unitOfWork, IEntireEvaluationService entireEvaluationService,IReferentialApplicantService referentialApplicantService)
        {
            _unitOfWork = unitOfWork;
            _entireEvaluationService = entireEvaluationService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست ارزیابی های به عمل آمده از متقاضی", PreservedRouteParameters = "ApplicantId",Key = "EntireEvaluation_List")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _entireEvaluationService.GetPagedListAsync(new EntireEvaluationSearchRequest
            {
                ApplicantId = ApplicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EntireEvaluationSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();

            var viewModel = await _entireEvaluationService.GetPagedListAsync(request);
            if (viewModel.EntireEvaluations == null || !viewModel.EntireEvaluations.Any()) return Content("no-more-info");
            return PartialView(MVC.EntireEvaluation.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual async Task<ActionResult> Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();

            var viewModel = await _entireEvaluationService.GetForCreate(ApplicantId);
            return PartialView(MVC.EntireEvaluation.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج ارزیابی جدید")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        public virtual async Task<ActionResult> Create(AddEntireEvaluationViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                await _entireEvaluationService.FillAddViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.EntireEvaluation.Views._Create, viewModel)
                    }
                };
            }
            viewModel.Content = viewModel.Content.ToSafeHtml();
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.StrongPoint = viewModel.StrongPoint.ToSafeHtml();
            viewModel.Foible = viewModel.Foible.ToSafeHtml();
            var newEntireEvaluation = await _entireEvaluationService.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.EntireEvaluation.Views._EntireEvaluationItem, newEntireEvaluation)
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
            var viewModel = await _entireEvaluationService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            return View( viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش ارزیابی")]
        
        public virtual async Task<ActionResult> Edit(EditEntireEvaluationViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!await _entireEvaluationService.IsInDb(viewModel.Id))
                this.AddErrors("Content", "ارزیابی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                await _entireEvaluationService.FillEditViewModel(viewModel);
                return View(viewModel);
            }

            viewModel.Content = viewModel.Content.ToSafeHtml();
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.StrongPoint = viewModel.StrongPoint.ToSafeHtml();
            viewModel.Foible = viewModel.Foible.ToSafeHtml();
            await _entireEvaluationService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue())
                this.AddErrors("Content", string.Format(message, "ارزیابی"));

            if (ModelState.IsValid) return RedirectToAction(MVC.EntireEvaluation.List(viewModel.ApplicantId));
            await _entireEvaluationService.FillEditViewModel(viewModel);
            return View(viewModel);
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "حذف ارزیابی به عمل آمده از متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _entireEvaluationService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region GetDocument
        public virtual async Task<ActionResult> GetDocument(Guid id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();

            var data = await _entireEvaluationService.GaetAttachment(id);

            return File(data, "application/pdf", $"{id}.pdf");
        }
        #endregion
    }
}
