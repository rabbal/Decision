using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
using Decision.ViewModel.Presenter;
using Decision.Web.Filters;

namespace Decision.Web.Controllers
{
    [RoutePrefix("Applicant/Presenter")]
    [Route("{action}")]
    public partial class PresenterController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPresenterService _PresenterService;
        #endregion

        #region	Ctor
        public PresenterController(IUnitOfWork unitOfWork, IPresenterService PresenterService)
        {
            _unitOfWork = unitOfWork;
            _PresenterService = PresenterService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [Mvc5Authorize()]
        public virtual async Task<ActionResult> List()
        {
            return View();
        }
        //[CheckReferrer]
        [AjaxOnly]
        [Mvc5Authorize()]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax()
        {
            return PartialView();
        }
        #endregion

        #region Create
        [HttpGet]
        [AjaxOnly]
        [Mvc5Authorize()]
        public virtual async Task<ActionResult> Create()
        {
            return View();
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize()]
        public virtual async Task<ActionResult> Create(AddPresenterViewModel viewModel)
        {
            return View();
        }
        #endregion

        #region Edit
        [Route("Edit/{id}")]
        [HttpGet]
        [AjaxOnly]
        [Mvc5Authorize()]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            return View();
        }

        [HttpPost]
        [Route("Edit/{id}")]
        //[CheckReferrer]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize()]
        public virtual async Task<ActionResult> Edit(EditPresenterViewModel viewModel)
        {
            return View();
        }

        #endregion

        #region Delete
        [HttpPost]
        [AjaxOnly]
        [Route("Delete/{id}")]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [Mvc5Authorize()]

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {

            return View();
        }
        #endregion

        #region RemoteValidations
        [HttpPost]
        [AjaxOnly]
        [Mvc5Authorize()]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public async Task<JsonResult> Check(string input, Guid? id)
        {
            return Json(true);
        }
        #endregion

        #region PrivateMethods

        #endregion
    }
}
