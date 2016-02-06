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
using Decision.Web.Extentions;
using Decision.Web.Filters;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.TrainingCourse;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("BaseSetting/TrainingCenter/TrainingCenter")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageTrainingCourse)]
    public partial class TrainingCourseController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingCourseService _trainingCourseService;
        #endregion

        #region	Ctor
        public TrainingCourseController(IUnitOfWork unitOfWork, ITrainingCourseService trainingCourseService)
        {
            _unitOfWork = unitOfWork;
            _trainingCourseService = trainingCourseService;
        }
        #endregion

        #region ListAjax,List

        //[CheckReferrer]
        [HttpGet]
        [Route("{trainingCenterId}/Courses")]
        [MvcSiteMapNode(ParentKey = "TrainingCenter_List", Title = "لیست دوره ها",PreservedRouteParameters = "trainingCenterId")]
        public virtual async Task<ActionResult> List(Guid? trainingCenterId)
        {
            if (trainingCenterId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel =
                await
                    _trainingCourseService.GetPagedListAsync(new TrainingCourseSearchRequest
                    {
                        TrainingCenterId = trainingCenterId.Value
                    });
            return View(viewModel);
        }

        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TrainingCourseSearchRequest request)
        {
            var viewModel = await _trainingCourseService.GetPagedListAsync(request);
            if (viewModel.TrainingCourses == null || !viewModel.TrainingCourses.Any())
                return Content("no-more-info");
            return PartialView(MVC.TrainingCourse.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual ActionResult Create(Guid centerId)
        {
            var viewModel = new AddTrainingCourseViewModel
            {
                TrainingCenterId = centerId
            };
            return PartialView(MVC.TrainingCourse.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        public virtual async Task<ActionResult> Create(AddTrainingCourseViewModel viewModel)
        {
            if (await _trainingCourseService.IsExistCourseCode(viewModel.CourseCode, null, viewModel.TrainingCenterId))
                this.AddErrors("CourseCode", "یک دوره با این کد برای مرکز قبلا در سیستم ثبت شده است");
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString(MVC.TrainingCourse.Views._Create, viewModel)
                        }
                };
            }
            var newCourse =await  _trainingCourseService.Create(viewModel);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                        View = this.RenderPartialViewToString(MVC.TrainingCourse.Views._TrainingCourseItem, newCourse)
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
            var viewModel = await _trainingCourseService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.TrainingCourse.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش ")]
        public virtual async Task<ActionResult> Edit(EditTrainingCourseViewModel viewModel)
        {
            if (await _trainingCourseService.IsExistCourseCode(viewModel.CourseCode, viewModel.Id, viewModel.TrainingCenterId))
                this.AddErrors("CourseCode", "یک دوره با این کد برای مرکز قبلا در سیستم ثبت شده است");
            if (!await _trainingCourseService.IsInDb(viewModel.Id))
                this.AddErrors("CourseCode", "دوره کارآموزی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View = this.RenderPartialViewToString(MVC.TrainingCourse.Views._Edit, viewModel)
                        }
                };
            }
            await _trainingCourseService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("CourseCode", string.Format(message, "دروه کارآموزی"));

            if (!ModelState.IsValid)
                return new JsonNetResult
                {
                    Data =
                        new
                        {
                            success = false,
                            View =
                                this.RenderPartialViewToString(MVC.TrainingCourse.Views._Edit, viewModel)
                        }
                };
            var course = await _trainingCourseService.GetTrainingCourseViewModel(viewModel.Id);
            return new JsonNetResult
            {
                Data =
                    new
                    {
                        success = true,
                        View = this.RenderPartialViewToString(MVC.TrainingCourse.Views._TrainingCourseItem, course)
                    }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _trainingCourseService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region GetByCenterId
        [Mvc5Authorize]
        [HttpGet]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> GetCourses(Guid id)
        {
            return new JsonNetResult
            {
                Data = await _trainingCourseService.GetAsSelectListByTrainingCenterIdAsync(id, null),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _trainingCourseService.GetTrainingCourseViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.TrainingCourse.Views._TrainingCourseItem, viewModel);
        }
        #endregion

        #region PrivateMethods

        #endregion

    }
}
