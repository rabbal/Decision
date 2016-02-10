using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.Common.Extentions;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.EducationalExperience;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    [RoutePrefix("Applicant/EducationalExperience")]
    [Route("{action}")]
   // [Mvc5Authorize(AssignableToRolePermissions.CanManageEducationalExperience)]
    public partial class EducationalExperienceController : Controller
    {
        #region	Fields

        private readonly IApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEducationalExperienceService _educationalExperienceService;
        #endregion

        #region	Ctor
        public EducationalExperienceController(IUnitOfWork unitOfWork, IEducationalExperienceService educationalExperienceService,IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _educationalExperienceService = educationalExperienceService;
            _userManager = userManager;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست سوابق پژوهشی متقاضی", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _educationalExperienceService.GetPagedListAsync(new EducationalExperienceSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EducationalExperienceSearchRequest request)
        {
            var viewModel = await _educationalExperienceService.GetPagedListAsync(request);
            if (viewModel.EducationalExperiences == null || !viewModel.EducationalExperiences.Any())
                return Content("no-more-info");

            return PartialView(MVC.EducationalExperience.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual  ActionResult Create(Guid applicantId)
        {
            var viewModel = new AddEducationalExperienceViewModel {ApplicantId = applicantId};
            return PartialView(MVC.EducationalExperience.Views._Create,viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج سابقه تدریس در مراکر علمی برای متقاضی")]
        public virtual async Task<ActionResult> Create(AddEducationalExperienceViewModel viewModel)
        {
             if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.EducationalExperience.Views._Create, viewModel)
                    }
                };
            }
            var newEducationalExperience = await _educationalExperienceService.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View =
                        this.RenderPartialViewToString(MVC.EducationalExperience.Views._EducationalExperienceItem, newEducationalExperience)
                }
            };
        }
        #endregion

        #region Edit
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _educationalExperienceService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
             return PartialView(MVC.EducationalExperience.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش سابقه تدریس در مراکر علمی")]
        public virtual async Task<ActionResult> Edit(EditEducationalExperienceViewModel viewModel)
        {
            if (!await _educationalExperienceService.IsInDb(viewModel.Id))

                if (!ModelState.IsValid)
                {
                    return new JsonNetResult
                    {
                        Data = new
                        {
                            success = false,
                            View =
                                this.RenderPartialViewToString(MVC.EducationalExperience.Views._Edit, viewModel)
                        }
                    };
                }

            await _educationalExperienceService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _userManager.GetCurrentUserId());

            var educationalExperience =
                    await _educationalExperienceService.GetEducationalExperienceViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View =
                            this.RenderPartialViewToString(MVC.EducationalExperience.Views._EducationalExperienceItem, educationalExperience)
                    }
                };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "حذف سابقه تدریس در مراکر علمی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id,Guid applicantId)
        {
             await _educationalExperienceService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _educationalExperienceService.GetEducationalExperienceViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.EducationalExperience.Views._EducationalExperienceItem, viewModel);
        }
        #endregion
    }
}
