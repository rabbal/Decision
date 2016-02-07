
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Decision.Common.Filters;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.PrivateMessage;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.ViewModel.PrivateMessage;
using MvcSiteMapProvider;

namespace Decision.Web.Controllers
{
    
    [RoutePrefix("PrivateMessage")]
    [Route("{action}")]
    [Mvc5Authorize(AssignableToRolePermissions.CanUsePrivateMessage)]
    public partial class PrivateMessageController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageService _messageService;
        private readonly IApplicationUserManager _userManager;
        #endregion

        #region	Ctor
        public PrivateMessageController(IUnitOfWork unitOfWork, IMessageService messageService, IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _messageService = messageService;
            _userManager = userManager;
        }
        #endregion

        #region Conversations
        [HttpGet]
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "لیست پیغام های دریافتی")]
        public virtual async Task<ActionResult> InBox()
        {
            var conversations = await _messageService.GetInBox(1);
            return View(conversations);
        }
        [AjaxOnly]
        public virtual async Task<ActionResult> InBoxAjax(int pageIndex)
        {
            var conversations = await _messageService.GetInBox(pageIndex);
            if (conversations == null || !conversations.Any()) return Content("no-more-info");
            return PartialView(MVC.PrivateMessage.Views._InBoxListAjax, conversations);
        }
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "لیست پیغام های ارسالی")]
        public virtual async Task<ActionResult> OutBox()
        {
            var conversations = await _messageService.GetOutBox(1);
            return View(conversations);
        }
        [AjaxOnly]
        public virtual async Task<ActionResult> OutBoxAjax(int pageIndex)
        {
            var conversations = await _messageService.GetOutBox(pageIndex);
            if (conversations == null || !conversations.Any()) return Content("no-more-info");
            return PartialView(MVC.PrivateMessage.Views._OutBoxListAjax, conversations);
        }
        #endregion

        #region Messages
        [Route("Messages/{conversationId}")]
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "گفتگو ها")]
        public virtual async Task<ActionResult> Messages(Guid conversationId)
        {
            var messages = await _messageService.GetMessages(conversationId);
            if (messages == null) return HttpNotFound();
            return View(messages);
        }
        #endregion

        #region Create
        [HttpGet]
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "ارسال پیغام جدید",HttpMethod = "GET")]
        public virtual async Task<ActionResult> NewMessage()
        {
            var viewModel = new AddConversationViewModel
            {
                Users = await _userManager.GetAsSelectListItem()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        [AllowUploadSafeFiles]
        [MvcSiteMapNode(ParentKey = "Home_Index", Title = "ارسال پیغام جدید",HttpMethod = "POST")]
        public virtual async Task<ActionResult> NewMessage(AddConversationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _messageService.Create(viewModel);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.PrivateMessage.OutBox());
        }

        [HttpPost]
        [AllowUploadSafeFiles]
        [ValidateAntiForgeryToken]
        //[CheckReferrer]
        public virtual async Task<ActionResult> Reply(AddMessageViewModel viewModel)
        {
            if (!await _messageService.CheckAccess(viewModel.ConversationId))
                return HttpNotFound();
            _messageService.Create(viewModel);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(MVC.PrivateMessage.Messages(viewModel.ConversationId));
        }
        #endregion
        
        #region Delete
        [HttpPost]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            await _messageService.DeleteAsync(id.Value);
            return Content("ok");
        }
        #endregion
    }
}