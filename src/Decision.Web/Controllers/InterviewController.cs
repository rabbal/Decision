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
using Decision.Common.Extentions;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Interview;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/Interview")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageInterview)]
    public partial class InterviewController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInterviewService _interviewService;
        #endregion

        #region	Ctor
        public InterviewController(IUnitOfWork unitOfWork, IInterviewService interviewService)
        {
            _unitOfWork = unitOfWork;
            _interviewService = interviewService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست مصاحبه ها متقاضی", PreservedRouteParameters = "ApplicantId",Key = "InterView_List")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {

            var viewModel = await _interviewService.GetPagedListAsync(new InterviewSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(InterviewSearchRequest request)
        {
           
            var viewModel = await _interviewService.GetPagedListAsync(request);
            if (viewModel.Interviews == null || !viewModel.Interviews.Any()) return Content("no-more-info");
            return PartialView(MVC.Interview.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create(Guid applicantId)
        {
            var viewModel = new AddInterviewViewModel {ApplicantId = applicantId};
            return PartialView(MVC.Interview.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج مصاحبه جدید")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        public virtual async Task<ActionResult> Create(AddInterviewViewModel viewModel)
        {
            
            if (!ModelState.IsValid)
            {
                
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Interview.Views._Create, viewModel)
                    }
                };
            }
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Body = viewModel.Body.ToSafeHtml();
            var newInterview = await _interviewService.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Interview.Views._InterviewItem, newInterview)
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
            var viewModel = await _interviewService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            
            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش مصاحبه")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        
        public virtual async Task<ActionResult> Edit(EditInterviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Body = viewModel.Body.ToSafeHtml();
            await _interviewService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync(); return RedirectToAction(MVC.Interview.List(viewModel.ApplicantId));
          
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "حذف مصاحبه به عمل آمده از متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id,Guid applicantId)
        {
            
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _interviewService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

    }
}
