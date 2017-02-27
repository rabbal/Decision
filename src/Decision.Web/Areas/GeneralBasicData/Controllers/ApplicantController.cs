using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.Framework.Domain.Entities;
using Decision.Framework.GuardToolkit;
using Decision.Framework.MvcToolkit.Filters;
using Decision.ServiceLayer.Interfaces.Applicants;
using Decision.ViewModels.GeneralBasicData.Applicants;
using ControllerBase = Decision.Framework.MvcToolkit.Controller.ControllerBase;

namespace Decision.Web.Areas.GeneralBasicData.Controllers
{
    public partial class ApplicantController : ControllerBase
    {
        #region Fields
        private readonly IApplicantService _applicantService;

        #endregion

        #region Constructor

        public ApplicantController(IApplicantService applicantService)
        {
            Check.ArgumentNotNull(applicantService, nameof(applicantService));

            _applicantService = applicantService;
        }

        #endregion

        #region List
        [HttpGet, MvcAuthorize]
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.GeneralBasicData.Applicant.List());
        }
        [HttpGet, MvcAuthorize]
        public virtual async Task<ActionResult> List()
        {
            var listViewModel = await _applicantService.FetchListAsync(new ApplicantListRequest());

            return View(listViewModel);
        }

        [HttpPost, AjaxOnly, MvcAuthorize, NoOutputCache]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> List(ApplicantListRequest request)
        {
            var listViewModel = await _applicantService.FetchListAsync(request);

            return PartialView(MVC.GeneralBasicData.Applicant.Views._ItemsList, listViewModel);
        }
        #endregion

        #region Create
        [HttpGet, MvcAuthorize]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost, MvcAuthorize, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CreateApplicantViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _applicantService.Create(model);
            return View();
        }
        #endregion

        #region Edit
        [HttpGet, MvcAuthorize]
        public virtual async Task<ActionResult> Edit(long? id)
        {
            if (!id.HasValue) return HttpBadRequest();

            var model = await _applicantService.FetchForEditAsync(id.Value);

            return View(model);
        }

        [HttpPost, MvcAuthorize, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(EditApplicantViewModel model)
        {
            var exist = await _applicantService.ExistsAsync(model.Id);
            if (!exist) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            await _applicantService.EditAsync(model);
            return View();
        }
        #endregion

        #region Delete
        [HttpPost, MvcAuthorize]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Delete(Entity model)
        {
            await _applicantService.DeleteAsync(model);
            return RedirectToAction("List");
        }
        #endregion

        #region RemoteValidations

        #endregion
    }
}
