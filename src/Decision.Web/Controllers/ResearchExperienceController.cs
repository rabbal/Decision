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
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.ResearchExperience;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/ResearchExperience")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageResearchExperience)]
    public partial class ResearchExperienceController : Controller
    {
	    #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResearchExperienceService _researchExperienceService;
        #endregion

        #region	Ctor
        public ResearchExperienceController(IReferentialApplicantService referentialApplicantService,IUnitOfWork unitOfWork, IResearchExperienceService ResearchExperienceService)
        {
            _unitOfWork = unitOfWork;
            _researchExperienceService = ResearchExperienceService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست سوابق پژوهشی متقاضی", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _researchExperienceService.GetPagedListAsync(new ResearchExperienceSearchRequest
            {
                ApplicantId =  ApplicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ResearchExperienceSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel = await _researchExperienceService.GetPagedListAsync(request);
            if (viewModel.ResearchExperiences == null || !viewModel.ResearchExperiences.Any())
                return Content("no-more-info");

            return PartialView(MVC.ResearchExperience.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual ActionResult Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = new AddResearchExperienceViewModel
            {
                ApplicantId = ApplicantId
            };
            return PartialView(MVC.ResearchExperience.Views._Create,viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج سابقه پروژهشی برای متقاضی")]
        public virtual async Task<ActionResult> Create(AddResearchExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.ResearchExperience.Views._Create, viewModel)
                    }
                };
            }

           var newResearch=await _researchExperienceService.Create(viewModel);
           return new JsonNetResult
           {
               Data = new
               {
                   success =true,
                   View =
                       this.RenderPartialViewToString(MVC.ResearchExperience.Views._ResearchExperienceItem, newResearch)
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
            var viewModel = await _researchExperienceService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            return PartialView(MVC.ResearchExperience.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش  ")]
        
        public virtual async Task<ActionResult> Edit(EditResearchExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!await _researchExperienceService.IsInDb(viewModel.Id))
                this.AddErrors("Title", "سابقه پژوهشی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.ResearchExperience.Views._Edit, viewModel)
                    }
                };
            }

            await _researchExperienceService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("Title", string.Format(message, "سابقه پژوهشی"));

            if (!ModelState.IsValid)
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.ResearchExperience.Views._Edit, viewModel)
                    }
                };

            var research = await _researchExperienceService.GetResearchExperienceViewModel(viewModel.Id);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View =
                        this.RenderPartialViewToString(MVC.ResearchExperience.Views._ResearchExperienceItem, research)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "سابقه پژوهشی ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            await _researchExperienceService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _researchExperienceService.GetResearchExperienceViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.ResearchExperience.Views._ResearchExperienceItem, viewModel);
        }
        #endregion
    }
}
