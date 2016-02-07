using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Controller;
using Decision.Common.Extentions;
using Decision.Common.Filters;
using Decision.Common.Json;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Evaluations;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.Question;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{

    [RoutePrefix("BaseSetting/Question")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanManageQuestion)]
    public partial class QuestionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionService _questionService;
        #endregion

        #region	Ctor
        public QuestionController(IUnitOfWork unitOfWork, IQuestionService questionService)
        {
            _unitOfWork = unitOfWork;
            _questionService = questionService;
        }
        #endregion

        #region List,ListAjax
        [HttpGet]
        [MvcSiteMapNode(ParentKey = "Base_Index", Title = "مدیریت سوالات")]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _questionService.GetPagedListAsync(new QuestionSearchRequest());
            return View(viewModel);
        }
        //[CheckReferrer]
        [HttpPost]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(QuestionSearchRequest request)
        {
            var viewModel = await _questionService.GetPagedListAsync(request);
            if (viewModel.Questions == null || !viewModel.Questions.Any()) return Content("no-more-info");
            return PartialView(MVC.Question.Views._ListAjax, viewModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual async Task<ActionResult> Create()
        {
            return PartialView(MVC.Question.Views._Create);
        }

        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [Activity(Description = "درج سوال جدید")]
        public virtual async Task<ActionResult> Create(AddQuestionViewModel viewModel)
        {
           // if ((viewModel.Type == QuestionType.CheckBoxList || viewModel.Type == QuestionType.RadioButtonList) && viewModel.Options == null)
            {
                this.AddErrors("Title", "برای سوال از نوع چند گزینه ای لازم است گزنیه درج کنید");
            }
            if (!ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = new
                    {
                        success = false,
                        View = this.RenderPartialViewToString(MVC.Question.Views._Create, viewModel)
                    }
                };
            }
            var newQuestion = await _questionService.CreateAsync(viewModel);
            return new JsonNetResult
            {
                Data = new
                {
                    success = true,
                    View = this.RenderPartialViewToString(MVC.Question.Views._QuestionItem, newQuestion)
                }
            };
        }
        #endregion

        #region Edit
        [HttpGet]
        [Route("Edit/{id}")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = await _questionService.GetForEditAsync(id.Value);
            if (viewModel == null) return HttpNotFound();
            return View(viewModel);
        }

        [HttpPost]
        //[CheckReferrer]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        [Activity(Description = "ویرایش سوال")]
        public virtual async Task<ActionResult> Edit(EditQuestionViewModel viewModel)
        {
            if (!await _questionService.IsInDb(viewModel.Id))
                this.AddErrors("Body", "سوال مورد نظر توسط یکی از کاربران در شبکه،حذف شده است");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await _questionService.EditAsync(viewModel);
            await _unitOfWork.SaveAllChangesAsync();
            return RedirectToAction(MVC.Question.List());
        }

        #endregion

        #region enable/disable
        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [Activity(Description = "غیر فعال کردن سوال")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Disable(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _questionService.DisableAsync(id.Value);
            var question = await _questionService.GetQuestionViewModel(id.Value);
            return PartialView(MVC.Question.Views._QuestionItem, question);
        }

        [HttpPost]
        [AjaxOnly]
        //[CheckReferrer]
        [Activity(Description = "فعال کردن سوال")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Enable(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _questionService.EnableAsync(id.Value);
            var question = await _questionService.GetQuestionViewModel(id.Value);
            return PartialView(MVC.Question.Views._QuestionItem, question);
        }
        #endregion
    }
}
