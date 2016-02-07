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
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkExperienceService _workExperienceService;
        #endregion

        #region	Ctor
        public WorkExperienceController(IUnitOfWork unitOfWork, IWorkExperienceService workExperienceService)
        {
            _unitOfWork = unitOfWork;
            _workExperienceService = workExperienceService;

        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست سوابق کاری متقاضی", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _workExperienceService.GetPagedListAsync(new WorkExperienceSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(WorkExperienceSearchRequest request)
        {
            var viewModel = await _workExperienceService.GetPagedListAsync(request);
            if (viewModel.WorkExperiences == null || !viewModel.WorkExperiences.Any()) return Content("no-more-info");
            return PartialView(MVC.WorkExperience.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create(Guid applicantId)
        {
            var viewModel = await _workExperienceService.GetForCreate(applicantId, IranCitiesPath);
            return PartialView(MVC.WorkExperience.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج سابقه کاری")]
        public virtual async Task<ActionResult> Create(AddWorkExperienceViewModel viewModel)
        {
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
            var newWork = await _workExperienceService.Create(viewModel);

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
           
            return PartialView(MVC.WorkExperience.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش سابقه کاری ")]
        public virtual async Task<ActionResult> Edit(EditWorkExperienceViewModel viewModel)
        {

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
            await _unitOfWork.SaveAllChangesAsync();
            
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

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Activity(Description = "سابقه کاری ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id, Guid applicantId)
        {
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
