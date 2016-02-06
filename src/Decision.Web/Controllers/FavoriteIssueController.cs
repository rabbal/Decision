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
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.EducationalExperience;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/FavoriteIssue")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageFavoriteIssue)]
    public partial class FavoriteIssueController : Controller
    {
	    #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEducationalExperienceService _educationalExperienceService;
        #endregion

        #region	Ctor
        public FavoriteIssueController(IUnitOfWork unitOfWork, IEducationalExperienceService educationalExperienceService,IReferentialApplicantService referentialApplicantService)
        {
            _unitOfWork = unitOfWork;
            _educationalExperienceService = educationalExperienceService;
            _referentialApplicantService = referentialApplicantService;
        }

        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست موضوعات مورد علاقه", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _educationalExperienceService.GetPagedListAsync(new EducationalExperienceSearchRequest
            {
                ApplicantId = ApplicantId,
                Type = EducationalExperienceType.FavoriteIssue
            });
            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(EducationalExperienceSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();

            request.Type=EducationalExperienceType.FavoriteIssue;
            var viewModel = await _educationalExperienceService.GetPagedListAsync(request);
            if (viewModel.EducationalExperiences == null || !viewModel.EducationalExperiences.Any())
                return Content("no-more-info");

            return PartialView(MVC.FavoriteIssue.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();

            var viewModel =
                await
                    _educationalExperienceService.GetForCreate(ApplicantId,
                        EducationalExperienceType.FavoriteIssue);
            return PartialView(MVC.FavoriteIssue.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج موضوع مورد علاقه برای متقاضی")]
        public virtual async Task<ActionResult> Create(AddEducationalExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!ModelState.IsValid)
            {
                await _educationalExperienceService.FillAddViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.FavoriteIssue.Views._Create, viewModel)
                    }
                };
            }
            var newFavoriteIssue = await _educationalExperienceService.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View =
                        this.RenderPartialViewToString(MVC.FavoriteIssue.Views._FavoriteIssueItem, newFavoriteIssue)
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
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            return PartialView(MVC.FavoriteIssue.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش موضوع مورد علاقه متقاضی")]
        public virtual async Task<ActionResult> Edit(EditEducationalExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!await _educationalExperienceService.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "موضوع مورد علاقه مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                await _educationalExperienceService.FillEditViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.FavoriteIssue.Views._Edit, viewModel)
                    }
                };
            }

            await _educationalExperienceService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("TitleId", string.Format(message, "اولویت تصویب شده برای متقاضی"));

            if (ModelState.IsValid)
            {
                var FavoriteIssue =
                    await _educationalExperienceService.GetEducationalExperienceViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View =
                            this.RenderPartialViewToString(MVC.FavoriteIssue.Views._FavoriteIssueItem, FavoriteIssue)
                    }
                };
            }
            await _educationalExperienceService.FillEditViewModel(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View =
                        this.RenderPartialViewToString(MVC.FavoriteIssue.Views._Edit, viewModel)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "موضوع مورد علاقه متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _educationalExperienceService.DeleteAsync(id.Value);
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
            return PartialView(MVC.FavoriteIssue.Views._FavoriteIssueItem, viewModel);
        }
        #endregion
    }
}
