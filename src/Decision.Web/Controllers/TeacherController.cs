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
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Teacher;
using Decision.ViewModel.ReferentialTeacher;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Auth = Decision.ServiceLayer.Security.AssignableToRolePermissions;
namespace Decision.Web.Controllers
{
    /// <summary>
    /// کنترلر مربوط به استاد
    /// </summary>
    [RoutePrefix("Teacher/Manage")]
    [Route("{action}")]
    public partial class TeacherController : Controller
    {
        #region Fields

        private readonly IReferentialTeacherService _referentialTeacherService;
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly ITitleService _titleService;
        private readonly ITrainingCenterService _trainingCenterService;
        private readonly ITrainingCourseService _trainingCourseService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherService _TeacherService;
        private readonly IApplicationUserManager _userManager;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        #endregion

        #region Ctor

        public TeacherController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IReferentialTeacherService referentialTeacherService,
            ITrainingCenterService trainingCenterService, ITrainingCourseService trainingCourseService, ITitleService titleService,
            ITeacherService TeacherService, IStateService stateService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _TeacherService = TeacherService;
            _stateService = stateService;
            _cityService = cityService;
            _trainingCenterService = trainingCenterService;
            _titleService = titleService;
            _referentialTeacherService = referentialTeacherService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Audit(Description = "مشاهده لیست اساتید")]
        [Mvc5Authorize(Auth.CanViewTeacherList)]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _TeacherService.GetPagedListAsync(new TeacherSearchRequest());
            viewModel.Positions = await _titleService.GetAsSelectListItemAsync(TitleType.TeacherPosition, null);
            viewModel.States = _stateService.GetAsSelectListItemAsync(string.Empty, IranCitiesPath);
            viewModel.TrainingCenters = await _trainingCenterService.GetAsSelectListItemAsync(string.Empty, null);
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [Mvc5Authorize(Auth.CanViewTeacherList)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TeacherSearchRequest request)
        {
            var viewModel = await _TeacherService.GetPagedListAsync(request);
            if (viewModel.Teachers == null || !viewModel.Teachers.Any()) return Content("no-more-info");
            return PartialView(MVC.Teacher.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [Mvc5Authorize(Auth.CanCreateTeacher)]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = await _TeacherService.GetForCreate(IranCitiesPath);
            return View(viewModel);
        }

        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize(Auth.CanCreateTeacher)]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        public virtual async Task<ActionResult> Create(AddTeacherViewModel viewModel)
        {
            if (!viewModel.NationalCode.IsValidNationalCode())
                this.AddErrors("NationalCode", "لطفا کد ملی را به شکل صحیح وارد کنید");

            if (await _TeacherService.IsTeacherNationalCodeExist(viewModel.NationalCode, null))
                this.AddErrors("NationalCode", "یک استاد بااین کد ملی قبلا در سیستم ثبت شده است");
           
            if (!ModelState.IsValid)
            {
                await _TeacherService.FillAddViewMoel(viewModel, IranCitiesPath);

                return View(viewModel);
            }

            _TeacherService.Create(viewModel);
            await _unitOfWork.SaveChangesAsync();
           this.NotyInformation("استاد جدید با موفقیت ثبت شد.");
            return RedirectToAction(MVC.Teacher.Create());
        }
        #endregion

        #region Edit
        [Route("EditTeacher/{TeacherId}")]
        [HttpGet]
        [Mvc5Authorize(Auth.CanEditTeacher)]
        [TeacherAuthorize]
        public virtual async Task<ActionResult> Edit(Guid TeacherId)
        {
            var viewModel = await _TeacherService.GetForEditAsync(TeacherId, IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }

        [Route("EditTeacher/{TeacherId}")]
        [TeacherAuthorize]
        [HttpPost]
        // [CheckReferrer]
        [ValidateAntiForgeryToken]
        [AllowUploadSpecialFilesOnly(".png,.jpg,.jpeg,.gif", justImage: true)]
        [Mvc5Authorize(Auth.CanEditTeacher)]
        public virtual async Task<ActionResult> Edit(EditTeacherViewModel viewModel)
        {
            if (!viewModel.NationalCode.IsValidNationalCode())
                this.AddErrors("NationalCode", "لطفا کد ملی را به شکل صحیح وارد کنید");

            if (await _TeacherService.IsTeacherNationalCodeExist(viewModel.NationalCode, viewModel.Id))
                this.AddErrors("NationalCode", "یک استاد بااین کد ملی قبلا در سیستم ثبت شده است");
           
            if (!ModelState.IsValid)
            {
                await _TeacherService.FillEditViewMoel(viewModel, IranCitiesPath);
                return View(viewModel);
            }

            if (!await _TeacherService.IsInDb(viewModel.Id))
                this.AddErrors("FirstName", "استاد مورد نظر توسط یکی از کاربران در شبکه، حذف شده است");

            if (!ModelState.IsValid)
            {
                await _TeacherService.FillEditViewMoel(viewModel, IranCitiesPath);
                return View(MVC.Teacher.Views.Edit, viewModel);
            }

            await _TeacherService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();

            if (message.HasValue())
            {
                this.AddErrors("FirstName", string.Format(message, "استاد"));
            }

            if (ModelState.IsValid)
                return _userManager.IsOperator()
                    ? RedirectToAction(MVC.Teacher.Details(viewModel.Id))
                    : RedirectToAction(MVC.Teacher.List());

            await _TeacherService.FillEditViewMoel(viewModel, IranCitiesPath);
            return View(MVC.Teacher.Views.Edit, viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize(Auth.CanDeleteTeacher)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            if (!_referentialTeacherService.CanManageTeacher(id)) return HttpNotFound();
            await _TeacherService.DeleteAsync(id);
            return Content("ok");
        }
        #endregion

        #region Details
        [HttpGet]
        [Mvc5Authorize(Auth.CanViewTeacherDetails)]
        [Route("Details/{TeacherId}")]
        [TeacherAuthorize]
        [SiteMapTitle("FullName", Target = AttributeTarget.CurrentNode)]
        public virtual async Task<ActionResult> Details(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            var viewModel = await _TeacherService.GetTeacherDetails(TeacherId);
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
            var viewModel =await  _TeacherService.GetRefersTeachers(true);
            return View(viewModel);
        }
        
        [HttpGet]
        [Mvc5Authorize(StandardRoles.Operators)]
        [Audit(Description = "مشاهده لیست اساتید درج شده  توسط خود اپراتور")]
        public virtual async Task<ActionResult> NewTeacherList()
        {
            var viewModel = await _TeacherService.GetRefersTeachers(false);
            return View(viewModel);
        }

        [HttpPost]
        [Mvc5Authorize(StandardRoles.Operators)]
        [Audit(Description = "اتمام نهایی کار استاد ارجاع داده شده")]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> FinishRefer(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            await _TeacherService.FinishedRefer(TeacherId);
            return Content("ok");
        }

        #endregion

        #region RemoteValidations
        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditTeacher, Auth.CanCreateTeacher)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<JsonResult> IsTeacherBirthCertificateNumberExist(string birthCertificateNumber, Guid? id)
        {
            return Json(!await _TeacherService.IsTeacherBirthCertificateNumberExist(birthCertificateNumber, id));
        }

        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize(Auth.CanEditTeacher, Auth.CanCreateTeacher)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<JsonResult> IsTeacherNationalCodeExist(string nationalCode, Guid? id)
        {
            var validation = nationalCode.IsValidNationalCode();
            if (!validation) return Json(false);
            return Json(!await _TeacherService.IsTeacherNationalCodeExist(nationalCode, id));
        }

        #endregion

        #region DownloadFiles
        [HttpGet]
        [Route("File/{TeacherId}/{type}")]
        [Mvc5Authorize(Auth.CanViewTeacherDetails)]
        [TeacherAuthorize]
        public virtual async Task<ActionResult> GetTeacherFile(Guid TeacherId, string type)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            var file = await _TeacherService.GetTeacherDocument(TeacherId, type);
            return File(file, "image/png", $"{TeacherId}.png");
        }
        #endregion

        #region ApproveTeacher
        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanApproveTeacher)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ApproveTeacher(Guid id)
        {
            var viewModel = await _TeacherService.Approve(id);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Teacher.Views._TeacherItem, viewModel);
        }
        #endregion

        #region Refer
        [HttpGet]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanReferTeacher)]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Refer(Guid TeacherId)
        {
            if (await _TeacherService.IsInRefer(TeacherId)) return Content("in-refer");
            var viewModel = new AddReferentialTeacherViewModel
            {
                TeacherId = TeacherId,
                RefrencedToUsers = await _userManager.GetAsSelectListItem()
            };
            return PartialView(MVC.Teacher.Views._Refer, viewModel);
        }
        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize()]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Refer(AddReferentialTeacherViewModel model)
        {
            if (!ModelState.IsValid)
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Teacher.Views._Refer, model)
                    }
                };

            var viewModel = await _TeacherService.ReferTeacher(model);
            if (viewModel == null) return HttpNotFound();
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Teacher.Views._TeacherItem, viewModel)
                }
            };
        }

        [HttpPost]
        //[CheckReferrer]
        [Mvc5Authorize(Auth.CanCancelReferTeacher)]
        public virtual async Task<ActionResult> CancelRefer(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = await _TeacherService.CancelRefer(id.Value);
            if (viewModel == null) return HttpNotFound();

            return PartialView(MVC.Teacher.Views._TeacherItem, viewModel);
        }
        #endregion
        
    }
}
