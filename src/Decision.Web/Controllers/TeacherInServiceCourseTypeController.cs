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
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.TeacherInServiceCourseType;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Teacher/TeacherInServiceCourseType")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageTeacherInServiceCourseType)]
    public partial class TeacherInServiceCourseTypeController : Controller
    {
        #region	Fields

        private readonly IReferentialTeacherService _referentialTeacherService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherInServiceCourseTypeService _TeacherInServiceCourseType;
        #endregion

        #region	Ctor
        public TeacherInServiceCourseTypeController(IReferentialTeacherService referentialTeacherService,IUnitOfWork unitOfWork, ITeacherInServiceCourseTypeService TeacherInServiceCourseTypeService)
        {
            _unitOfWork = unitOfWork;
            _TeacherInServiceCourseType = TeacherInServiceCourseTypeService;
            _referentialTeacherService = referentialTeacherService;
        }
        #endregion

        #region List,ListAjax
        [Route("List/{TeacherId}")]
        [HttpGet]
        [TeacherAuthorize]
        [MvcSiteMapNode(ParentKey = "Teacher_Details", Title = "لیست دوره های ضمن خدمت", PreservedRouteParameters = "TeacherId")]
        public virtual async Task<ActionResult> List(Guid TeacherId)
        {
            var viewModel = await _TeacherInServiceCourseType.GetPagedListAsync(new TeacherInServiceCourseTypeSearchRequest
            {
                TeacherId = TeacherId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TeacherInServiceCourseTypeSearchRequest request)
        {
            if (!_referentialTeacherService.CanManageTeacher(request.TeacherId)) return HttpNotFound();
            var viewModel = await _TeacherInServiceCourseType.GetPagedListAsync(request);
            if (viewModel.TeacherInServiceCourseTypes == null || !viewModel.TeacherInServiceCourseTypes.Any())
                return Content("no-more-info");

            return PartialView(MVC.TeacherInServiceCourseType.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            var viewModel =
                await
                    _TeacherInServiceCourseType.GetForCreate(TeacherId);
            return PartialView(MVC.TeacherInServiceCourseType.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج دوره ضمن خدمت")]
        public virtual async Task<ActionResult> Create(AddTeacherInServiceCourseTypeViewModel viewModel)
        {
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();
            if (!ModelState.IsValid)
            {
                await _TeacherInServiceCourseType.FillAddViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.TeacherInServiceCourseType.Views._Create, viewModel)
                    }
                };
            }
            var newTeacherInServiceCourseType = await _TeacherInServiceCourseType.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View =
                        this.RenderPartialViewToString(MVC.TeacherInServiceCourseType.Views._TeacherInServiceCourseTypeItem, newTeacherInServiceCourseType)
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
            var viewModel = await _TeacherInServiceCourseType.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            if (!_referentialTeacherService.CanManageTeacher(viewModel.TeacherId)) return HttpNotFound();
            return PartialView(MVC.TeacherInServiceCourseType.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش دوره ضمن خدمت")]
        public virtual async Task<ActionResult> Edit(EditTeacherInServiceCourseTypeViewModel viewModel)
        {
            if (!await _TeacherInServiceCourseType.IsInDb(viewModel.Id))
                this.AddErrors("TitleId", "دوزه ضمن خدمت مورد نظر توسط یکی از کاربران در شبکه ، حذف  است");

            if (!ModelState.IsValid)
            {
                await _TeacherInServiceCourseType.FillEditViewModel(viewModel);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View =
                            this.RenderPartialViewToString(MVC.TeacherInServiceCourseType.Views._Edit, viewModel)
                    }
                };
            }

            await _TeacherInServiceCourseType.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("TitleId", string.Format(message, "دوره ضمن خدمت"));

            if (ModelState.IsValid)
            {
                var TeacherInServiceCourseType =
                    await _TeacherInServiceCourseType.GetTeacherInServiceCourseTypeViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View =
                            this.RenderPartialViewToString(MVC.TeacherInServiceCourseType.Views._TeacherInServiceCourseTypeItem, TeacherInServiceCourseType)
                    }
                };
            }
            await _TeacherInServiceCourseType.FillEditViewModel(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View =
                        this.RenderPartialViewToString(MVC.TeacherInServiceCourseType.Views._Edit, viewModel)
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
        public virtual async Task<ActionResult> Delete(Guid TeacherId)
        {
            if (!_referentialTeacherService.CanManageTeacher(TeacherId)) return HttpNotFound();
            await _TeacherInServiceCourseType.DeleteAsync(TeacherId);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _TeacherInServiceCourseType.GetTeacherInServiceCourseTypeViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.TeacherInServiceCourseType.Views._TeacherInServiceCourseTypeItem, viewModel);
        }
        #endregion
    }
}
