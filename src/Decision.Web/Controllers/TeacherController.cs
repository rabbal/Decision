using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.Common.Helpers.Extentions;
using Decision.Common.Helpers.Json;
using Decision.Common.Validations;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Applicant;
using Decision.ViewModel.ReferentialApplicant;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Auth = Decision.ServiceLayer.Security.AssignableToRolePermissions;
namespace Decision.Web.Controllers
{
    /// <summary>
    /// کنترلر مربوط به متقاضی
    /// </summary>
    [RoutePrefix("Applicant/Manage")]
    [Route("{action}")]
    public partial class ApplicantController : Controller
    {
        #region Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly ITitleService _titleService;
        private readonly ITrainingCenterService _trainingCenterService;
        private readonly ITrainingCourseService _trainingCourseService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicantService _ApplicantService;
        private readonly IApplicationUserManager _userManager;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        #endregion

        #region Ctor

        public ApplicantController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IReferentialApplicantService referentialApplicantService,
            ITrainingCenterService trainingCenterService, ITrainingCourseService trainingCourseService, ITitleService titleService,
            IApplicantService ApplicantService, IStateService stateService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _ApplicantService = ApplicantService;
            _stateService = stateService;
            _cityService = cityService;
            _trainingCenterService = trainingCenterService;
            _titleService = titleService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Audit(Description = "مشاهده لیست اساتید")]
        [Mvc5Authorize(Auth.CanViewApplicantList)]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _ApplicantService.GetPagedListAsync(new ApplicantSearchRequest());
            viewModel.Positions = await _titleService.GetAsSelectListItemAsync(TitleType.ApplicantPosition, null);
            viewModel.States = _stateService.GetAsSelectListItemAsync(string.Empty, IranCitiesPath);
            viewModel.TrainingCenters = await _trainingCenterService.GetAsSelectListItemAsync(string.Empty, null);
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ApplicantSearchRequest request)
        {
            var viewModel = await _ApplicantService.GetPagedListAsync(request);
            if (viewModel.Applicants == null || !viewModel.Applicants.Any()) return Content("no-more-info");
            return PartialView(MVC.Applicant.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [Mvc5Authorize(Auth.CanCreateApplicant)]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = await _ApplicantService.GetForCreate(IranCitiesPath);
            return View(viewModel);
        }

        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize(Auth.CanCreateApplicant)]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        public virtual async Task<ActionResult> Create(AddApplicantViewModel viewModel)
        {
            if (!viewModel.NationalCode.IsValidNationalCode())
                this.AddErrors("NationalCode", "لطفا کد ملی را به شکل صحیح وارد کنید");

            if (await _ApplicantService.IsApplicantNationalCodeExist(viewModel.NationalCode, null))
                this.AddErrors("NationalCode", "یک متقاضی بااین کد ملی قبلا در سیستم ثبت شده است");
           
            if (!ModelState.IsValid)
            {
                await _ApplicantService.FillAddViewMoel(viewModel, IranCitiesPath);

                return View(viewModel);
            }

            _ApplicantService.Create(viewModel);
            await _unitOfWork.SaveChangesAsync();
           this.NotyInformation("متقاضی جدید با موفقیت ثبت شد.");
            return RedirectToAction(MVC.Applicant.Create());
        }
        #endregion

        #region Edit
        [Route("EditApplicant/{ApplicantId}")]
        [HttpGet]
        [Mvc5Authorize(Auth.CanEditApplicant)]
        [ApplicantAuthorize]
        public virtual async Task<ActionResult> Edit(Guid ApplicantId)
        {
            var viewModel = await _ApplicantService.GetForEditAsync(ApplicantId, IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }

        [Route("EditApplicant/{ApplicantId}")]
        [ApplicantAuthorize]
        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        [Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(EditApplicantViewModel viewModel)
        {
            if (!viewModel.NationalCode.IsValidNationalCode())
                this.AddErrors("NationalCode", "لطفا کد ملی را به شکل صحیح وارد کنید");

            if (await _ApplicantService.IsApplicantNationalCodeExist(viewModel.NationalCode, viewModel.Id))
                this.AddErrors("NationalCode", "یک متقاضی بااین کد ملی قبلا در سیستم ثبت شده است");
           
            if (!ModelState.IsValid)
            {
                await _ApplicantService.FillEditViewMoel(viewModel, IranCitiesPath);
                return View(viewModel);
            }

            if (!await _ApplicantService.IsInDb(viewModel.Id))
                this.AddErrors("FirstName", "متقاضی مورد نظر توسط یکی از کاربران در شبکه، حذف شده است");

            if (!ModelState.IsValid)
            {
                await _ApplicantService.FillEditViewMoel(viewModel, IranCitiesPath);
                return View(MVC.Applicant.Views.Edit, viewModel);
            }

            await _ApplicantService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();

            if (message.HasValue())
            {
                this.AddErrors("FirstName", string.Format(message, "متقاضی"));
            }

            if (ModelState.IsValid)
                return _userManager.IsOperator()
                    ? RedirectToAction(MVC.Applicant.Details(viewModel.Id))
                    : RedirectToAction(MVC.Applicant.List());

            await _ApplicantService.FillEditViewMoel(viewModel, IranCitiesPath);
            return View(MVC.Applicant.Views.Edit, viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize(Auth.CanDeleteApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            if (!_referentialApplicantService.CanManageApplicant(id)) return HttpNotFound();
            await _ApplicantService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region Details
        [HttpGet]
        [Mvc5Authorize(Auth.CanViewApplicantDetails)]
        [Route("Details/{ApplicantId}")]
        [ApplicantAuthorize]
        [SiteMapTitle("FullName", Target = AttributeTarget.CurrentNode)]
        public virtual async Task<ActionResult> Details(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = await _ApplicantService.GetApplicantDetails(ApplicantId);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Refer Operations
        [HttpGet]
        [Mvc5Authorize(StandardRoles.Operators)]
        [Audit(Description = "مشاهده لیست ارجاعات خود")]
        public virtual async Task<ActionResult> ReferList()
        {
            var viewModel =await  _ApplicantService.GetRefersApplicants(true);
            return View(viewModel);
        }
        
        [HttpGet]
        [Mvc5Authorize(StandardRoles.Operators)]
        [Audit(Description = "مشاهده لیست اساتید درج شده  توسط خود اپراتور")]
        public virtual async Task<ActionResult> NewApplicantList()
        {
            var viewModel = await _ApplicantService.GetRefersApplicants(false);
            return View(viewModel);
        }

        [HttpPost]
        [Mvc5Authorize(StandardRoles.Operators)]
        [Audit(Description = "اتمام نهایی کار متقاضی ارجاع داده شده")]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> FinishRefer(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            await _ApplicantService.FinishedRefer(ApplicantId);
            return Content("ok");
        }

        #endregion

        #region RemoteValidations
        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditApplicant, Auth.CanCreateApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<JsonResult> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            return Json(!await _ApplicantService.IsApplicantBirthCertificateNumberExist(birthCertificateNumber, id));
        }

        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditApplicant, Auth.CanCreateApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<JsonResult> IsApplicantNationalCodeExist(string nationalCode, Guid? id)
        {
            var validation = nationalCode.IsValidNationalCode();
            if (!validation) return Json(false);
            return Json(!await _ApplicantService.IsApplicantNationalCodeExist(nationalCode, id));
        }

        #endregion

        #region DownloadFiles
        [HttpGet]
        [Route("File/{ApplicantId}/{type}")]
        [Mvc5Authorize(Auth.CanViewApplicantDetails)]
        [ApplicantAuthorize]
        public virtual async Task<ActionResult> GetApplicantFile(Guid ApplicantId, string type)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var file = await _ApplicantService.GetApplicantDocument(ApplicantId, type);
            return File(file, "image/png", $"{ApplicantId}.png");
        }
        #endregion

        #region ApproveApplicant
        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanApproveApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ApproveApplicant(Guid id)
        {
            var viewModel = await _ApplicantService.Approve(id);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Applicant.Views._ApplicantItem, viewModel);
        }
        #endregion

        #region Refer
        [HttpGet]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanReferApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Refer(Guid ApplicantId)
        {
            if (await _ApplicantService.IsInRefer(ApplicantId)) return Content("in-refer");
            var viewModel = new AddReferentialApplicantViewModel
            {
                ApplicantId = ApplicantId,
                RefrencedToUsers = await _userManager.GetAsSelectListItem()
            };
            return PartialView(MVC.Applicant.Views._Refer, viewModel);
        }
        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize()]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Refer(AddReferentialApplicantViewModel model)
        {
            if (!ModelState.IsValid)
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Applicant.Views._Refer, model)
                    }
                };

            var viewModel = await _ApplicantService.ReferApplicant(model);
            if (viewModel == null) return HttpNotFound();
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Applicant.Views._ApplicantItem, viewModel)
                }
            };
        }

        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanCancelReferApplicant)]
        public virtual async Task<ActionResult> CancelRefer(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _ApplicantService.CancelRefer(id.Value);
            if (viewModel == null) return HttpNotFound();

            return PartialView(MVC.Applicant.Views._ApplicantItem, viewModel);
        }
        #endregion
        
    }
}
