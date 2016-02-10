using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Address;
using Decision.Web.Filters;
using MvcSiteMapProvider;
using Decision.Common.Extentions;
using Decision.ServiceLayer.Contracts.Users;

namespace Decision.Web.Controllers
{
    [RoutePrefix("Applicant/Address")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageAddress)]
    public partial class AddressController : Controller
    {
        #region	Fields
        
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        private readonly IApplicationUserManager _userManager;
        #endregion

        #region	Ctor
        public AddressController(IUnitOfWork unitOfWork, IAddressService addressService,IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
            _userManager = userManager;

        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست آدرس ها", PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid applicantId)
        {
            var viewModel = await _addressService.GetAddressesAsync(new AddressSearchRequest
            {
                ApplicantId = applicantId
            });
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]

        public virtual async Task<ActionResult> ListAjax(AddressSearchRequest request)
        {
            var viewModel = await _addressService.GetAddressesAsync(request);
            if (viewModel.Addresses == null || !viewModel.Addresses.Any()) return Content("no-more-info");
            return PartialView(MVC.Address.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]

        public virtual ActionResult Create(Guid applicantId)
        {
            var viewModel = _addressService.GetForCreate(applicantId, IranCitiesPath);
            return PartialView(MVC.Address.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج آدرس جدید")]

        public virtual async Task<ActionResult> Create(AddAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _addressService.FillAddViewModel(viewModel, IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Address.Views._Create, viewModel)
                    }
                };
            }
            var newAdress = await _addressService.Create(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Address.Views._AddressItem, newAdress)
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
            var viewModel = await _addressService.GetForEditAsync(id.Value, IranCitiesPath);
            if (viewModel == null) return HttpNotFound();
          
            return PartialView(MVC.Address.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش آدرس متقاضی")]
        public virtual async Task<ActionResult> Edit(EditAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _addressService.FillEditViewModel(viewModel, IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Address.Views._Edit, viewModel)
                    }
                };
            }
            await _addressService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync(auditUserId: _userManager.GetCurrentUserId());

            if (ModelState.IsValid)
            {
                var address = await _addressService.GetAddressViewModel(viewModel.Id);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = true,
                        View = this.RenderPartialViewToString(MVC.Address.Views._AddressItem, address)
                    }
                };
            }
            _addressService.FillEditViewModel(viewModel, IranCitiesPath);
            return new JsonNetResult
            {
                Data = new
                {
                    success = false,
                    View = this.RenderPartialViewToString(MVC.Address.Views._Edit, viewModel)
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
        public virtual async Task<ActionResult> Delete(Guid? id, Guid applicantId)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _addressService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion

        #region CancelEdit
        [HttpPost]
        [AjaxOnly]
        public virtual async Task<ActionResult> CancelEdit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _addressService.GetAddressViewModel(id.Value);
            if (viewModel == null) return HttpNotFound();
            return PartialView(MVC.Address.Views._AddressItem, viewModel);
        }
        #endregion
    }
}
