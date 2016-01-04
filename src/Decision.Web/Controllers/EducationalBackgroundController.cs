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
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.EducationalBackground;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Teacher/EducationalBackground")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageEducationalBackground)]
    public partial class EducationalBackgroundController : Controller
    {
        #region	Fields

        private readonly IReferentialTeacherService _referentialTeacherService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEducationalBackgroundService _educationalBackgroundService;
        #endregion

        #region	Ctor
        public EducationalBackgroundController(IUnitOfWork unitOfWork, IEducationalBackgroundService educationalBackgroundService,IReferentialTeacherService referentialTeacherService)
        {
            _unitOfWork = unitOfWork;
            _educationalBackgroundService = educationalBackgroundService;
            _referentialTeacherService = referentialTeacherService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{TeacherId}")]
        [TeacherAuthorize]
        [MvcSiteMapNode(ParentKey = "Teacher_Details", Title = "لیست سوابق تحصیلی ها", PreservedRouteParameters = "TeacherId")]
        public virtual async Task<ActionResult> List(Guid TeacherId)
        {
            var viewModel = await _educationalBackgroundService.GetPagedListAsync(new EducationalBackgroundSearchRequest
            {
                TeacherId = TeacherId
            });
            return View( viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EducationalBackgroundSearchRequest request)
        {
            if (!_referentialTeacherService.CanManageTeacher(request.TeacherId)) return HttpNotFound();

            var viewModel = await _educationalBackgroundService.GetPagedListAsync(request);
            if (viewModel.EducationalBackgrounds == null || !viewModel.EducationalBackgrounds.Any())
                return Content("no-more-info");
            return PartialView(MVC.EducationalBackground.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual async Task<ActionResult> Create(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            var viewModel =
                await
                    _educationalBackgroundService.GetForCreate(TeacherId);
            return PartialView(MVC.EducationalBackground.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج سابقه تحصیلی برای استاد")]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg", justImage: true)]
        public virtual async Task<ActionResult> Create(AddEducationalBackgroundViewModel viewModel)
        {
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                await _educationalBackgroundService.FillAddViewModel(viewModel);

                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.EducationalBackground.Views._Create, viewModel)
                    }
                };
            }
          var newbackground=await  _educationalBackgroundService.Create(viewModel);
            
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.EducationalBackground.Views._EducationalBackgroundItem, newbackground)
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
            var viewModel = await _educationalBackgroundService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            return View( viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش سابقه تحصیلی استاد")]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg", justImage: true)]
        public virtual async Task<ActionResult> Edit(EditEducationalBackgroundViewModel viewModel)
        {
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();

            if (!await _educationalBackgroundService.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "سابقه تحصیلی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                await _educationalBackgroundService.FillEditViewModel(viewModel);
                return View( viewModel);
            }

            await _educationalBackgroundService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("TitleId", string.Format(message, "سابقه تحصیلی استاد"));

            if (ModelState.IsValid)
                return RedirectToAction(MVC.EducationalBackground.List(viewModel.TeacherId));

            await _educationalBackgroundService.FillEditViewModel(viewModel);
            return View( viewModel);
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "سابقه تحصیلی استاد")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id,Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _educationalBackgroundService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

    }
}
