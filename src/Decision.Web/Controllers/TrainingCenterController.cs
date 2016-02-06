using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.Web.Extentions;
using System.Collections.Generic;
using System.Linq;
using Decision.Common.Controller;
using Decision.Common.Helpers.Extentions;
using Decision.Common.Helpers.Json;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.TrainingCenter;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("BaseSetting/TrainingCenter")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageTrainingCenter)]
    public partial class TrainingCenterController : Controller
    {
        #region	Fields

        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingCenterService _trainingCenterService;
        #endregion

        #region	Ctor
        public TrainingCenterController(ICityService cityService, IUnitOfWork unitOfWork, ITrainingCenterService TrainingCenterService, IStateService StateService)
        {
            _unitOfWork = unitOfWork;
            _trainingCenterService = TrainingCenterService;
            _stateService = StateService;
            _cityService = cityService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Audit(Description = "")]
        [MvcSiteMapNode(ParentKey = "Base_Index", Title = "مدیریت مراکز کارآموزی ها",Key = "TrainingCenter_List")]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _trainingCenterService.GetPagedListAsync(new TrainingCenterSearchRequest());
            viewModel.States = _stateService.GetAsSelectListItemAsync(null, IranCitiesPath);
            viewModel.Cities = new List<SelectListItem>();
            return View(viewModel);
        }

        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TrainingCenterSearchRequest request)
        {
            var viewModel = await _trainingCenterService.GetPagedListAsync(request);
            if (viewModel.TrainingCenters == null || !viewModel.TrainingCenters.Any()) return Content("no-more-info");
            return PartialView(MVC.TrainingCenter.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = await _trainingCenterService.GetForCreate(IranCitiesPath);
            return PartialView(MVC.TrainingCenter.Views._Create, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [AjaxOnly]
        [Audit(Description = "")]
        public virtual async Task<ActionResult> Create(AddTrainingCenterViewModel viewModel)
        {
            if (await _trainingCenterService.IsNameExistAsync(viewModel.CenterName, null, viewModel.City))
                this.AddErrors("CenterName", "یک موسسه کارآموزی با این نام در این شهر قبلا ثبت شده است");
            if (!ModelState.IsValid)
            {
                await _trainingCenterService.FillAddViewMolde(viewModel, IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.TrainingCenter.Views._TrainingCenterItem, viewModel)
                    }
                };
                
            }
            var newCenter = await _trainingCenterService.Create(viewModel);
         
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.TrainingCenter.Views._TrainingCenterItem, newCenter)
                }
            };
        }
        #endregion

        #region Edit
        [HttpGet]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _trainingCenterService.GetForEditAsync(id.Value,IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.TrainingCenter.Views._Edit,viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Property = "CenterName", Description = "ویرایش مرکز کارآموزی", LogType = AuditLogType.JustDescription,
            LoggableType = typeof(EditTrainingCenterViewModel))]
        public virtual async Task<ActionResult> Edit(EditTrainingCenterViewModel viewModel)
        {
            if (await _trainingCenterService.IsNameExistAsync(viewModel.CenterName, viewModel.Id, viewModel.City))
                this.AddErrors("CenterName", "یک مرکز کارآموزی با این نام در این شهر قبلا ثبت شده است");
            if (!await _trainingCenterService.IsInDb(viewModel.Id))
                this.AddErrors("CenterName", "مرکز کارآموزی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                await _trainingCenterService.FillEditViewMode(viewModel, IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.TrainingCenter.Views._Edit, viewModel)
                    }
                };
            }

            await _trainingCenterService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("CenterName", string.Format(message, "مرکز کارآموزی"));

            if (ModelState.IsValid)
            {
                var center = await _trainingCenterService.GetCenterViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View = this.RenderPartialViewToString(MVC.TrainingCenter.Views._TrainingCenterItem, center)
                    }
                };
            }
            await _trainingCenterService.FillEditViewMode(viewModel, IranCitiesPath);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View = this.RenderPartialViewToString(MVC.TrainingCenter.Views._Edit, viewModel)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _trainingCenterService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region Get as select list
        [Mvc5Authorize]
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> GetCenters(string id)
        {
            var result = await _trainingCenterService.GetAsSelectListItemAsync(id,null);
            return new JsonNetResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _trainingCenterService.GetCenterViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.TrainingCenter.Views._TrainingCenterItem, viewModel);
        }
        #endregion
    }
}
