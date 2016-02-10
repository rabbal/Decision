using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Extentions;
using Decision.Common.Filters;
using Decision.Common.Helpers;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.Applicant;
using Decision.Web.Filters;
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
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicantService _applicantService;
        private readonly IApplicationUserManager _userManager;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        #endregion

        #region Ctor

        public ApplicantController(IUnitOfWork unitOfWork, IApplicationUserManager userManager,

            IApplicantService applicantService, IStateService stateService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _applicantService = applicantService;
            _stateService = stateService;
            _cityService = cityService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Activity(Description = "مشاهده لیست اساتید")]
        [Mvc5Authorize(Auth.CanViewApplicantList)]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _applicantService.GetPagedListAsync(new ApplicantSearchRequest());
            viewModel.States = _stateService.GetAsSelectListItemAsync(string.Empty, IranCitiesPath);
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [Mvc5Authorize(Auth.CanViewApplicantList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ApplicantSearchRequest request)
        {
            var viewModel = await _applicantService.GetPagedListAsync(request);
            if (viewModel.Applicants == null || !viewModel.Applicants.Any()) return Content("no-more-info");
            return PartialView(MVC.Applicant.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [Mvc5Authorize(Auth.CanCreateApplicant)]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = await _applicantService.GetForCreate(IranCitiesPath);
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

            if (await _applicantService.IsApplicantNationalCodeExist(viewModel.NationalCode, null))
                this.AddErrors("NationalCode", "یک متقاضی بااین کد ملی قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                await _applicantService.FillAddViewMoel(viewModel, IranCitiesPath);

                return View(viewModel);
            }

            _applicantService.Create(viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId:_userManager.GetCurrentUserId());
            this.NotyInformation("متقاضی جدید با موفقیت ثبت شد.");
            return RedirectToAction(MVC.Applicant.Create());
        }
        #endregion

        #region Edit
        [Route("EditApplicant/{ApplicantId}")]
        [HttpGet]
        [Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(Guid applicantId)
        {
            var viewModel = await _applicantService.GetForEditAsync(applicantId, IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }

        [Route("EditApplicant/{ApplicantId}")]
        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        [Mvc5Authorize(Auth.CanEditApplicant)]
        public virtual async Task<ActionResult> Edit(EditApplicantViewModel viewModel)
        {
            if (!viewModel.NationalCode.IsValidNationalCode())
                this.AddErrors("NationalCode", "لطفا کد ملی را به شکل صحیح وارد کنید");

            if (await _applicantService.IsApplicantNationalCodeExist(viewModel.NationalCode, viewModel.Id))
                this.AddErrors("NationalCode", "یک متقاضی بااین کد ملی قبلا در سیستم ثبت شده است");

            if (!ModelState.IsValid)
            {
                await _applicantService.FillEditViewMoel(viewModel, IranCitiesPath);
                return View(MVC.Applicant.Views.Edit, viewModel);
            }
           this.NotySuccess("علمیات ویرایش متقاضی با موفقیت انجام شد");
            await _applicantService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId:_userManager.GetCurrentUserId());
            return
                RedirectToAction(MVC.Applicant.Details(viewModel.Id));
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
            await _applicantService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region Details
        [HttpGet]
        [Mvc5Authorize(Auth.CanViewApplicantDetails)]
        [Route("Details/{ApplicantId}")]

        [SiteMapTitle("FullName", Target = AttributeTarget.CurrentNode)]
        public virtual async Task<ActionResult> Details(Guid applicantId)
        {
            var viewModel = await _applicantService.GetApplicantDetails(applicantId);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region RemoteValidations
        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditApplicant, Auth.CanCreateApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<JsonResult> IsApplicantBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            return Json(!await _applicantService.IsApplicantBirthCertificateNumberExist(birthCertificateNumber, id));
        }

        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditApplicant, Auth.CanCreateApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<JsonResult> IsApplicantNationalCodeExist(string nationalCode, Guid? id)
        {
            var validation = nationalCode.IsValidNationalCode();
            return !validation
                ? Json(false)
                : Json(!await _applicantService.IsApplicantNationalCodeExist(nationalCode, id));
        }

        #endregion

        #region DownloadFiles
        [HttpGet]
        [Route("File/{ApplicantId}/{type}")]
        [Mvc5Authorize(Auth.CanViewApplicantDetails)]

        public virtual async Task<ActionResult> GetApplicantFile(Guid applicantId, string type)
        {
            var file = await _applicantService.GetApplicantDocument(applicantId, type);
            return File(file, "image/png", $"{applicantId}.png");
        }
        #endregion

        #region ApproveApplicant
        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanApproveApplicant)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ApproveApplicant(Guid id)
        {
            var viewModel = await _applicantService.Approve(id);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Applicant.Views._ApplicantItem, viewModel);
        }
        #endregion

    }
}
