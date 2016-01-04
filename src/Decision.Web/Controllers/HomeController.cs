using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.TeacherInfo;
using Decision.ServiceLayer.Contracts.PrivateMessage;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Home;

namespace Decision.Web.Controllers
{
    [Mvc5Authorize()]
    [Route("{action=index}")]
    public partial class HomeController : BaseController
    {
        #region Fields

        private readonly IMessageService _messageService;
        private readonly IApplicationUserManager _userManager;
        private readonly IArticleEvaluationService _ArticleEvaluationService;
        private readonly ITeacherService _TeacherService;
        private readonly IArticleService _ArticleService;

        #endregion

        #region Ctor

        public HomeController(IMessageService messageService, IApplicationUserManager userManager, IArticleService ArticleService, ITeacherService TeacherService, IArticleEvaluationService ArticleEvaluation)
        {
            _TeacherService = TeacherService;
            _ArticleEvaluationService = ArticleEvaluation;
            _ArticleService = ArticleService;
            _userManager = userManager;
            _messageService = messageService;
        }
        #endregion

        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult GetBenckMarks()
        {
            var viewModel = new BenchmarkViewModel
            {
                ApprovedTeachersCount = _TeacherService.ApprovedCount().GetPersianNumber(),
                NonApprovedTeachersCount = _TeacherService.NonApprovedCount().GetPersianNumber(),
                JugesCount = _TeacherService.Count().GetPersianNumber(),
                UsersCount = _userManager.Count().GetPersianNumber(),
                ArticlesCount = _ArticleService.Count().GetPersianNumber()
            };
            return PartialView(MVC.Home.Views._Benckmarks, viewModel);
        }

        [ChildActionOnly]
        public virtual ActionResult GetNewMessagesCount()
        {
            var messagesCount = _messageService.NewMessgesCount().GetPersianNumber();
            return PartialView(MVC.Shared.Views._NewMessagesCount, messagesCount);
        }

        #region Sections
        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult GetTopScoreTeachers()
        {
            var viewModel = _TeacherService.GetTenTopScoreTeachers();
            return PartialView(MVC.Home.Views._TeacherWithTopScoreList, viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult GetNewAddedTeachers()
        {
            var viewModel = _TeacherService.GetTenNewAddedTeachers();
            return PartialView(MVC.Home.Views._NewTeachersList, viewModel);
        }
        #endregion
    }
}