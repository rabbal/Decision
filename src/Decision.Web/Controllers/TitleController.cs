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
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Title;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("BaseSetting/Title")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageTitle)]
    public partial class TitleController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITitleService _titleService;
        #endregion

        #region	Ctor
        public TitleController(IUnitOfWork unitOfWork, ITitleService titleService)
        {
            _unitOfWork = unitOfWork;
            _titleService = titleService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Audit( Description = "")]
        [MvcSiteMapNode(ParentKey = "Base_Index", Title = "مدیریت عنوان ها")]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _titleService.GetPagedList(new TitleSearchRequest());
            return View(viewModel);
        }
        //[CheckReferrer]
        [AjaxOnly]
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TitleSearchRequest request)
        {
            var viewModel = await _titleService.GetPagedList(request);
            if (viewModel.Titles == null || !viewModel.Titles.Any()) return Content("no-more-info");
            return PartialView(MVC.Title.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        public virtual async Task<ActionResult> Create()
        {
            var viewModel = new AddTitleViewModel
            {
                Category = TitleCategory.InService,
                Type = TitleType.CourseContent,
                CategoryIsHidden =! await _titleService.IsEnableCategorySelection(TitleType.CourseContent)
            };
            return PartialView(MVC.Title.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "")]
        public virtual async Task<ActionResult> Create(AddTitleViewModel viewModel)
        {
            if (await _titleService.IsByNameExist(viewModel.Name, null, viewModel.Type, viewModel.Category))
                this.AddErrors("Name", "قبلا عنوانی با این نام ، نوع و گروه در سیستم ثبت شده است");
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Title.Views._Create, viewModel)
                    }
                };

            }
           var newTitle= await _titleService.Create(viewModel);

           return new JsonNetResult
           {
               Data = new
               {
                   success = true,
                   View = this.RenderPartialViewToString(MVC.Title.Views._TitleItem, newTitle )
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
            var viewModel = await _titleService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Title.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditTitleViewModel viewModel)
        {
            if (await _titleService.IsByNameExist(viewModel.Name, viewModel.Id, viewModel.Type, viewModel.Category))
                this.AddErrors("Name", "یک عنوان با این نام ، نوع و گروه در سیستم ثبت شده است");

            if(!await _titleService.IsInDb(viewModel.Id))
                this.AddErrors("Name", "عنوان مورد نظر توسط یکی از کاربران در شبکه، حذف شده است");

            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Title.Views._Edit, viewModel)
                    }
                };
                
            }

            await _titleService.EditAsync(viewModel);
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue()) this.AddErrors("Name", string.Format(message, "عنوان"));

            if (!ModelState.IsValid)
            {

                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Title.Views._Edit, viewModel)
                    }
                };
            }
            var title = await _titleService.GetTitleViewModel(viewModel.Id);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Title.Views._TitleItem, title)
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
            await _titleService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region RemoteValidations
       
        [HttpGet]
        [AjaxOnly]
        [Mvc5Authorize()]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> IsEnableCategory(TitleType type)
        {
            var isEnable = await _titleService.IsEnableCategorySelection(type);
            return isEnable ? Content("ok") : Content("nok");
        }

        #endregion

        #region PrivateMethods

        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _titleService.GetTitleViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Title.Views._TitleItem, viewModel);
        }
        #endregion

    }
}
