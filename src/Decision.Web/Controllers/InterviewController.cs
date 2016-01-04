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
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Interview;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Teacher/Interview")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageInterview)]
    public partial class InterviewController : Controller
    {
        #region	Fields

        private readonly IReferentialTeacherService _referentialTeacherService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInterviewService _interviewService;
        #endregion

        #region	Ctor
        public InterviewController(IUnitOfWork unitOfWork, IInterviewService interviewService,IReferentialTeacherService referentialTeacherService)
        {
            _unitOfWork = unitOfWork;
            _interviewService = interviewService;
            _referentialTeacherService = referentialTeacherService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{TeacherId}")]
        [TeacherAuthorize]
        [MvcSiteMapNode(ParentKey = "Teacher_Details", Title = "لیست مصاحبه ها استاد", PreservedRouteParameters = "TeacherId",Key = "InterView_List")]
        public virtual async Task<ActionResult> List(Guid TeacherId)
        {

            var viewModel = await _interviewService.GetPagedListAsync(new InterviewSearchRequest
            {
                TeacherId = TeacherId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(InterviewSearchRequest request)
        {
            if (!_referentialTeacherService.CanManageTeacher(request.TeacherId)) return HttpNotFound();

            var viewModel = await _interviewService.GetPagedListAsync(request);
            if (viewModel.Interviews == null || !viewModel.Interviews.Any()) return Content("no-more-info");
            return PartialView(MVC.Interview.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual async Task<ActionResult> Create(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();

            var viewModel = await _interviewService.GetForCreate(TeacherId);
            return PartialView(MVC.Interview.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج مصاحبه جدید")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        public virtual async Task<ActionResult> Create(AddInterviewViewModel viewModel)
        {
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                await _interviewService.FillAddViewModel(viewModel);
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
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش مصاحبه")]
        [AllowUploadSpecialFilesOnly(".pdf", justImage: false)]
        
        public virtual async Task<ActionResult> Edit(EditInterviewViewModel viewModel)
        {
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            if (!await _interviewService.IsInDb(viewModel.Id))
                this.AddErrors("Body", "مصاحبه مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                await _interviewService.FillEditViewModel(viewModel);
                return View(viewModel);
            }

            viewModel.Brief = viewModel.Brief.ToSafeHtml();
            viewModel.Body = viewModel.Body.ToSafeHtml();
            await _interviewService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue())
                this.AddErrors("Body", string.Format(message, "مصاحبه"));

            if (ModelState.IsValid) return RedirectToAction(MVC.Interview.List(viewModel.TeacherId));
            await _interviewService.FillEditViewModel(viewModel);
            return View(viewModel);
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "حذف مصاحبه به عمل آمده از استاد")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id,Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _interviewService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region GetDocument
        public virtual async Task<ActionResult> GetDocument(Guid id,Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            var data = await _interviewService.GetAttachment(id);
            return File(data, "application/pdf", $"{id}.{"pdf"}");
        }
        #endregion
    }
}
