using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Extentions;
using Decision.Common.Filters;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.EducationalBackground;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{

    [RoutePrefix("Applicant/EducationalBackground")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageEducationalBackground)]
    public partial class EducationalBackgroundController : Controller
    {
        #region	Fields

        private readonly IApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEducationalBackgroundService _educationalBackgroundService;
        #endregion

        #region	Ctor
        public EducationalBackgroundController(IUnitOfWork unitOfWork, IEducationalBackgroundService educationalBackgroundService,IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _educationalBackgroundService = educationalBackgroundService;
            _userManager = userManager;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست سوابق تحصیلی ها", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _educationalBackgroundService.GetPagedListAsync(new EducationalBackgroundSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EducationalBackgroundSearchRequest request)
        {
            var viewModel = await _educationalBackgroundService.GetPagedListAsync(request);
            if (viewModel.EducationalBackgrounds == null || !viewModel.EducationalBackgrounds.Any())
                return Content("no-more-info");
            return PartialView(MVC.EducationalBackground.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create(Guid applicantId)
        {
            var viewModel = new AddEducationalBackgroundViewModel {ApplicantId = applicantId};
            return PartialView(MVC.EducationalBackground.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج سابقه تحصیلی برای متقاضی")]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg", justImage: true)]
        public virtual async Task<ActionResult> Create(AddEducationalBackgroundViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {

                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.EducationalBackground.Views._Create, viewModel)
                    }
                };
            }
            var newbackground = await _educationalBackgroundService.Create(viewModel);

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

            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش سابقه تحصیلی متقاضی")]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg", justImage: true)]
        public virtual async Task<ActionResult> Edit(EditEducationalBackgroundViewModel viewModel)
        {
         
            if (!await _educationalBackgroundService.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "سابقه تحصیلی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _educationalBackgroundService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId:_userManager.GetCurrentUserId());

            return RedirectToAction(MVC.EducationalBackground.List(viewModel.ApplicantId));

        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "سابقه تحصیلی متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id, Guid applicantId)
        {
           
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _educationalBackgroundService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

    }
}
