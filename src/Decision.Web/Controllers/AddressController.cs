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
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Address;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("Applicant/Address")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageAddress)]
    public partial class AddressController : Controller
    {
        #region	Fields

        private readonly IReferentialApplicantService _referentialApplicantService;
        private const string IranCitiesPath = "~/App_Data/IranCities.xml";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        #endregion

        #region	Ctor
        public AddressController(IUnitOfWork unitOfWork, IAddressService addressService,IReferentialApplicantService referentialApplicantService)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
            _referentialApplicantService = referentialApplicantService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Route("List/{ApplicantId}")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        [ApplicantAuthorize]
        [MvcSiteMapNode(ParentKey = "Applicant_Details", Title = "لیست آدرس ها",PreservedRouteParameters = "ApplicantId")]
        public virtual async Task<ActionResult> List(Guid ApplicantId)
        {
            var viewModel = await _addressService.GetAddressesAsync(new AddressSearchRequest
            {
                ApplicantId = ApplicantId
            });
            return View( viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        
        public virtual async Task<ActionResult> ListAjax(AddressSearchRequest request)
        {
            if (!_referentialApplicantService.CanManageApplicant(request.ApplicantId)) return HttpNotFound();
            var viewModel = await _addressService.GetAddressesAsync(request);
            if (viewModel.Addresses == null || !viewModel.Addresses.Any()) return Content("no-more-info");
            return PartialView(MVC.Address.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        
        public virtual ActionResult Create(Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
            var viewModel = _addressService.GetForCreate(ApplicantId,IranCitiesPath);
            return PartialView(MVC.Address.Views._Create, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Audit(Description = "درج آدرس جدید")]
        
        public virtual async Task<ActionResult> Create(AddAddressViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            if (!ModelState.IsValid)
            {
                _addressService.FillAddViewModel(viewModel,IranCitiesPath);
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Address.Views._Create, viewModel)
                    }
                };
            }
            var newAdress=await _addressService.Create(viewModel);
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
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();
            return PartialView(MVC.Address.Views._Edit, viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Audit(Description = "ویرایش آدرس متقاضی")]
        public virtual async Task<ActionResult> Edit(EditAddressViewModel viewModel)
        {
            if (!_referentialApplicantService.CanManageApplicant(viewModel.ApplicantId)) return HttpNotFound();

            if (!await _addressService.IsInDb(viewModel.Id))
                this.AddErrors("Location", "آدرس مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

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
            var message = await _unitOfWork.ConcurrencySaveChangesAsync();
            if (message.HasValue())
                this.AddErrors("Location", string.Format(message, "آدرس"));

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
        [Audit(Description = "حذف آدرس متقاضی")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        
        public virtual async Task<ActionResult> Delete(Guid? id,Guid ApplicantId)
        {
            if (!_referentialApplicantService.CanManageApplicant(ApplicantId)) return HttpNotFound();
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
