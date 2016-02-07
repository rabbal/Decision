using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Extentions;
using Decision.Common.Filters;
using Decision.Common.HtmlCleaner;
using Decision.Common.Json;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntireEvaluationService _entireEvaluationService;
        #endregion

        #region	Ctor
        public EntireEvaluationController(IUnitOfWork unitOfWork, IEntireEvaluationService entireEvaluationService)
        {
            _unitOfWork = unitOfWork;
            _entireEvaluationService = entireEvaluationService;

        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        //[ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست ارزیابی های به عمل آمده از متقاضی", PreservedRouteParameters = "ApplicantId", Key = "EntireEvaluation_List")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _entireEvaluationService.GetPagedListAsync(new EntireEvaluationSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EntireEvaluationSearchRequest request)
        {
            var viewModel = await _entireEvaluationService.GetPagedListAsync(request);
            if (viewModel.EntireEvaluations == null || !viewModel.EntireEvaluations.Any()) return Content("no-more-info");
            return PartialView(MVC.EntireEvaluation.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual async Task<ActionResult> Create(Guid applicantId)
        {
            return PartialView(MVC.EntireEvaluation.Views._Create);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج ارزیابی جدید")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        public virtual async Task<ActionResult> Create(AddEntireEvaluationViewModel viewModel)
        {
           
            if (!ModelState.IsValid)
            {
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

            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش ارزیابی")]

        public virtual async Task<ActionResult> Edit(EditEntireEvaluationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.Content = viewModel.Content.ToSafeHtml();
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.StrongPoint = viewModel.StrongPoint.ToSafeHtml();
            viewModel.Foible = viewModel.Foible.ToSafeHtml();
            await _entireEvaluationService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync();
            return RedirectToAction(MVC.EntireEvaluation.List(viewModel.ApplicantId));

        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "حذف ارزیابی به عمل آمده از متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id, Guid applicantId)
        {
            
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _entireEvaluationService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion
    }
}
