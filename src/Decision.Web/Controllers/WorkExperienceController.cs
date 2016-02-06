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
using Decision.ViewModel.WorkExperience;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/WorkExperience")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageWorkExperience)]
    public partial class WorkExperienceController : Controller
    {
        #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkExperienceService _workExperienceService;
        #endregion

        #region	Ctor
        public WorkExperienceController(IUnitOfWork unitOfWork, IWorkExperienceService WorkExperienceService,IReferentialApplicantService referentialApplicantService)
        {
            _unitOfWork = unitOfWork;
            _workExperienceService = WorkExperienceService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست سوابق کاری متقاضی", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _workExperienceService.GetPagedListAsync(new WorkExperienceSearchRequest
            {
                ApplicantId = ApplicantId
            });
            return View( viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(WorkExperienceSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel = await _workExperienceService.GetPagedListAsync(request);
            if (viewModel.WorkExperiences == null || !viewModel.WorkExperiences.Any()) return Content("no-more-info");
            return PartialView(MVC.WorkExperience.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task< ActionResult> Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            
            var viewModel =await _workExperienceService.GetForCreate(ApplicantId, IranCitiesPath);
            return PartialView(MVC.WorkExperience.Views._Create,viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج سابقه کاری")]
        public virtual async Task<ActionResult> Create(AddWorkExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!ModelState.IsValid)
            {
               await _workExperienceService.FillAddViewModel(viewModel, IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.WorkExperience.Views._Create, viewModel)
                    }
                };

            }
          var newWork=await  _workExperienceService.Create(viewModel);
            
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.WorkExperience.Views._WorkExperienceItem, newWork)
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
            var viewModel = await _workExperienceService.GetForEditAsync(id.Value, IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            return PartialView(MVC.WorkExperience.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش سابقه کاری ")]
        public virtual async Task<ActionResult> Edit(EditWorkExperienceViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!await _workExperienceService.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "سابقه کاری مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
               await _workExperienceService.FillEditViewModel(viewModel, IranCitiesPath);
               return new JsonNetResult
               {
                   Data = new
                   {
                       success = false,
                       View = this.RenderPartialViewToString(MVC.WorkExperience.Views._Edit, viewModel)
                   }
               };
            }

            await _workExperienceService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("TitleId", string.Format(message, "سابقه کاری "));

            if (ModelState.IsValid)
            {
                var work = await _workExperienceService.GetWorkExperienceViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View = this.RenderPartialViewToString(MVC.WorkExperience.Views._WorkExperienceItem, work)
                    }
                };
            }

            await _workExperienceService.FillEditViewModel(viewModel, IranCitiesPath);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View = this.RenderPartialViewToString(MVC.WorkExperience.Views._Edit, viewModel)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "سابقه کاری ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            await _workExperienceService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _workExperienceService.GetWorkExperienceViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.WorkExperience.Views._WorkExperienceItem, viewModel);
        }
        #endregion
    }
}
