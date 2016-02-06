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
using Decision.ViewModel.Institution;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{

    [RoutePrefix("BaseSetting/Institution")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageInstitution)]
    public partial class InstitutionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInstitutionService _institutionService;
        #endregion

        #region	Ctor
        public InstitutionController(IUnitOfWork unitOfWork, IInstitutionService institutionService)
        {
            _unitOfWork = unitOfWork;
            _institutionService = institutionService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [MvcSiteMapNode(ParentKey = "Base_Index", Title = "مدیریت مراکز آموزش عالی")]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _institutionService.GetPagedListAsync(new InstitutionSearchRequest());
            return View( viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(InstitutionSearchRequest request)
        {
            var viewModel = await _institutionService.GetPagedListAsync(request);
            if (viewModel.Institutions == null || !viewModel.Institutions.Any()) return Content("no-more-info");
            return PartialView(MVC.Institution.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual ActionResult Create()
        {
            return PartialView(MVC.Institution.Views._Create);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج موسسه آموزشی")]
        public virtual async Task<ActionResult> Create(AddInstitutionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Institution.Views._Create, viewModel)
                    }
                };
            }
           var newInstitution =await _institutionService.Create(viewModel);

           return new JsonNetResult
           {
               Data = new
               {
                   success = true,
                   View = this.RenderPartialViewToString(MVC.Institution.Views._InstitutionItem, newInstitution)
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
            var viewModel = await _institutionService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Institution.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش موسسه آموزشی ")]
        public virtual async Task<ActionResult> Edit(EditInstitutionViewModel viewModel)
        {
            if (!await _institutionService.IsInDb(viewModel.Id))
                this.AddErrors("Name", "موسسه آموزشی مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Institution.Views._Edit, viewModel)
                    }
                };
            }

            await _institutionService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("Name", string.Format(message, "موسسه آموزشی "));

            if (!ModelState.IsValid)
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Institution.Views._Edit, viewModel)
                    }
                };

            var institution = await _institutionService.GetInstitutionViewModel(viewModel.Id);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Institution.Views._InstitutionItem, institution)
                }
            };
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Audit(Description = "موسسه آموزشی ")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _institutionService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _institutionService.GetInstitutionViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Institution.Views._InstitutionItem, viewModel);
        }
        #endregion
    }
}
