using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Decision.Common.Controller;
using Decision.Common.Filters;
using Decision.ServiceLayer.Contracts.Evaluations;
using Decision.ServiceLayer.Contracts.ApplicantInfo;
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
        
        private readonly IApplicantService _applicantService;
        private readonly IArticleService _articleService;

        #endregion

        #region Ctor

        public HomeController(IMessageService messageService, IApplicationUserManager userManager, IArticleService articleService, IApplicantService applicantService)
        {
            _applicantService = applicantService;
           
            _articleService = articleService;
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
                UsersCount = _userManager.Count().GetPersianNumber(),
                ArticlesCount = _articleService.Count().GetPersianNumber()
            };
            return PartialView(MVC.Home.Views._Benckmarks, viewModel);
        }

        [ChildActionOnly]
        public virtual ActionResult GetNewMessagesCount()
        {
            var messagesCount = _messageService.NewMessgesCount().GetPersianNumber();
            return PartialView(MVC.Shared.Views._NewMessagesCount, messagesCount);
        }

        //#region Sections
        //[AjaxOnly]
        //[HttpPost]
        //public virtual ActionResult GetTopScoreApplicants()
        //{
        //    var viewModel = _applicantService.GetTenTopScoreApplicants();
        //    return PartialView(MVC.Home.Views._ApplicantWithTopScoreList, viewModel);
        //}

        //[AjaxOnly]
        //[HttpPost]
        //public virtual ActionResult GetNewAddedApplicants()
        //{
        //    var viewModel = _applicantService.GetTenNewAddedApplicants();
        //    return PartialView(MVC.Home.Views._NewApplicantsList, viewModel);
        //}
        //#endregion
    }
}