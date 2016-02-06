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
using Decision.ViewModel.ApplicantInServiceCourseType;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/ApplicantInServiceCourseType")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageApplicantInServiceCourseType)]
    public partial class ApplicantInServiceCourseTypeController : Controller
    {
        #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicantInServiceCourseTypeService _ApplicantInServiceCourseType;
        #endregion

        #region	Ctor
        public ApplicantInServiceCourseTypeController(IReferentialApplicantService referentialApplicantService,IUnitOfWork unitOfWork, IApplicantInServiceCourseTypeService ApplicantInServiceCourseTypeService)
        {
            _unitOfWork = unitOfWork;
            _ApplicantInServiceCourseType = ApplicantInServiceCourseTypeService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [Route("List/{ApplicantId}")]
        [HttpGet]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست دوره های ضمن خدمت", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _ApplicantInServiceCourseType.GetPagedListAsync(new ApplicantInServiceCourseTypeSearchRequest
            {
                ApplicantId = ApplicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(ApplicantInServiceCourseTypeSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel = await _ApplicantInServiceCourseType.GetPagedListAsync(request);
            if (viewModel.ApplicantInServiceCourseTypes == null || !viewModel.ApplicantInServiceCourseTypes.Any())
                return Content("no-more-info");

            return PartialView(MVC.ApplicantInServiceCourseType.Views._ListAjax, viewModel);
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
                    _ApplicantInServiceCourseType.GetForCreate(ApplicantId);
            return PartialView(MVC.ApplicantInServiceCourseType.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج دوره ضمن خدمت")]
        public virtual async Task<ActionResult> Create(AddApplicantInServiceCourseTypeViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!ModelState.IsValid)
            {
                await _ApplicantInServiceCourseType.FillAddViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.ApplicantInServiceCourseType.Views._Create, viewModel)
                    }
                };
            }
            var newApplicantInServiceCourseType = await _ApplicantInServiceCourseType.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View =
                        this.RenderPartialViewToString(MVC.ApplicantInServiceCourseType.Views._ApplicantInServiceCourseTypeItem, newApplicantInServiceCourseType)
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
            var viewModel = await _ApplicantInServiceCourseType.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            return PartialView(MVC.ApplicantInServiceCourseType.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش دوره ضمن خدمت")]
        public virtual async Task<ActionResult> Edit(EditApplicantInServiceCourseTypeViewModel viewModel)
        {
            if (!await _ApplicantInServiceCourseType.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "دوزه ضمن خدمت مورد نظر توسط یکی از کاربران در شبکه ، حذف  است");

            if (!ModelState.IsValid)
            {
                await _ApplicantInServiceCourseType.FillEditViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.ApplicantInServiceCourseType.Views._Edit, viewModel)
                    }
                };
            }

            await _ApplicantInServiceCourseType.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("TitleId", string.Format(message, "دوره ضمن خدمت"));

            if (ModelState.IsValid)
            {
                var ApplicantInServiceCourseType =
                    await _ApplicantInServiceCourseType.GetApplicantInServiceCourseTypeViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View =
                            this.RenderPartialViewToString(MVC.ApplicantInServiceCourseType.Views._ApplicantInServiceCourseTypeItem, ApplicantInServiceCourseType)
                    }
                };
            }
            await _ApplicantInServiceCourseType.FillEditViewModel(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View =
                        this.RenderPartialViewToString(MVC.ApplicantInServiceCourseType.Views._Edit, viewModel)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "حذف دوره ضمن خدمت")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            await _ApplicantInServiceCourseType.DeleteAsync(ApplicantId);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _ApplicantInServiceCourseType.GetApplicantInServiceCourseTypeViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.ApplicantInServiceCourseType.Views._ApplicantInServiceCourseTypeItem, viewModel);
        }
        #endregion
    }
}
