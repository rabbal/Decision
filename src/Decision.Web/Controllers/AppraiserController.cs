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
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Appraiser;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
	
	[RoutePrefix("BaseSetting/Appraiser")]
	[Route("{action}")]
	[Mvc5Authorize(AssignableToRolePermissions.CanManageAppraiser)]
	public partial class AppraiserController : Controller
	{
	   #region	Fields
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAppraiserService _appraiserService;
		#endregion

		#region	Ctor
		public AppraiserController(IUnitOfWork unitOfWork, IAppraiserService appraiserService)
		{
			_unitOfWork = unitOfWork;
			_appraiserService = appraiserService;
		}
		#endregion

		#region List,ListAjax
		[HttpGet]
        [MvcSiteMapNode(ParentKey = "Base_Index", Title = "مدیریت ارزش گذاران")]
        public virtual async Task<ActionResult> List()
		{
			var viewModel = await _appraiserService.GetPagedListAsync(new AppraiserSearchRequest());
			return View( viewModel);
		}
		//[CheckReferrer]
		[HttpPost]
		[AjaxOnly]
		[OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
		public virtual async Task<ActionResult> ListAjax(AppraiserSearchRequest request)
		{
			var viewModel = await _appraiserService.GetPagedListAsync(request);
			if (viewModel.Appraisers == null || !viewModel.Appraisers.Any()) return Content("no-more-info");
			return PartialView(MVC.Appraiser.Views._ListAjax, viewModel);
		}
		#endregion

		#region Create
		[HttpGet]
		[AjaxOnly]
		public virtual async Task< ActionResult> Create()
		{
			var viewModel =await _appraiserService.GetForCreate();
			return PartialView(MVC.Appraiser.Views._Create, viewModel);
		}

		[AjaxOnly]
		[HttpPost]
		[ValidateAntiForgeryToken]
		//[CheckReferrer]
		[Audit(Description = "درج ارزش گذار جدید")]
		public virtual async Task<ActionResult> Create(AddAppraiserViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
			   await _appraiserService.FillAddViewModel(viewModel);
				return new JsonNetResult
				{
					Data = new
					{
						success = false,
						View = this.RenderPartialViewToString(MVC.Appraiser.Views._Create, viewModel)
					}
				};
			}
		  var newAppraiser= await _appraiserService.Create(viewModel);
		  return new JsonNetResult
		  {
			  Data = new
			  {
				  success = true,
				  View = this.RenderPartialViewToString(MVC.Appraiser.Views._AppraiserItem, newAppraiser)
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
			var viewModel = await _appraiserService.GetForEditAsync(id.Value);
			if (viewModel == null) return HttpNotFound();
			return PartialView(MVC.Appraiser.Views._Edit, viewModel);
		}

		[HttpPost]
		//[CheckReferrer]
		[AjaxOnly]
		[ValidateAntiForgeryToken]
		[Audit(Description = "ویرایش ارزش گذار")]
		public virtual async Task<ActionResult> Edit(EditAppraiserViewModel viewModel)
		{
			if (!await _appraiserService.IsInDb(viewModel.Id))
				this.AddErrors("FirstName", "ارزش گذار مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

			if (!ModelState.IsValid)
			{
			   await _appraiserService.FillEditViewModel(viewModel);
			   return new JsonNetResult
			   {
				   Data = new
				   {
					   success = false,
					   View = this.RenderPartialViewToString(MVC.Appraiser.Views._Edit, viewModel)
				   }
			   };
			}

			
			await _appraiserService.EditAsync(viewModel);
			var message = await _unitOfWork.ConcurrencySaveChangesAsync();
			if (message.HasValue())
				this.AddErrors("FirstName", string.Format(message, "ارزش گذار"));

			if (ModelState.IsValid)
			{
				var appraiser = await _appraiserService.GetAppraiserViewModel(viewModel.Id);
				return new JsonNetResult
				{
					Data = new
					{
						success = true,
						View = this.RenderPartialViewToString(MVC.Appraiser.Views._AppraiserItem, appraiser)
					}
				};
			}
		   await _appraiserService.FillEditViewModel(viewModel);
		   return new JsonNetResult
		   {
			   Data = new
			   {
				   success = false,
				   View = this.RenderPartialViewToString(MVC.Appraiser.Views._Edit, viewModel)
			   }
		   };
		}

		#endregion

		#region Delete
		[HttpPost]
		[AjaxOnly]
		//[CheckReferrer]
		[ValidateAntiForgeryToken]
		[Audit(Description = "حذف ارزش گذار")]
		[OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
		public virtual async Task<ActionResult> Delete(Guid? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			await _appraiserService.DeleteAsync(id.Value);
			return Content("ok");
		}
		#endregion

		#region CancelEdit
		[HttpPost]
		[AjaxOnly]
		public virtual async Task<ActionResult> CancelEdit(Guid? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var viewModel = await _appraiserService.GetAppraiserViewModel(id.Value);
			if (viewModel == null) return HttpNotFound();
			return PartialView(MVC.Appraiser.Views._AppraiserItem, viewModel);
		}
		#endregion
	}
}
