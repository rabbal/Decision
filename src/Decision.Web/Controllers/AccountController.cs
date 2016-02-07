using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using CaptchaMvc.Attributes;
using Decision.Common.Controller;
using Decision.Common.Extentions;
using Decision.Common.Filters;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Users;
using Decision.Utility;
using Decision.ViewModel.Account;
using Decision.Web.Extentions;
using Decision.Web.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Decision.Web.Controllers
{
    public partial class AccountController : BaseController
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IApplicationUserManager _userManager;
        #endregion

        #region Constructor

        public AccountController( IApplicationUserManager userManager, IUnitOfWork unitOfWork,
            IApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _unitOfWork = unitOfWork;
        }

        #endregion
     
        #region Login,LogOff
        [AllowAnonymous]
        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        //[CheckReferrer]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("تصویر امنیتی را درست وارد کنید")]
        [Activity(Description = "ورود به حساب کاربری",LogType = AuditLogType.Login)]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (_userManager.CheckIsUserBanned(model.UserName))
            {
                this.AddErrors("UserName", "حساب کاربری شما مسدود شده است");
                return View(model);
            }
          
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loggedinUser = await _userManager.FindAsync(model.UserName, model.Password);
            if (loggedinUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(loggedinUser.Id);
            }

            var result = await _signInManager.PasswordSignInAsync
                (model.UserName, model.Password, model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    this.AddErrors("UserName",
                        $"دقیقه دوباره امتحان کنید {_userManager.DefaultAccountLockoutTimeSpan} حساب شما قفل شد ! لطفا بعد از ");
                    return View(model);
                case SignInStatus.Failure:
                    this.AddErrors("UserName", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    this.AddErrors("Password", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    return View(model);
                default:
                    this.AddErrors("UserName",
                        "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید"
                       );
                    return View(model);
            }
        }

        [HttpGet]
        // [CheckReferrer]
        [Mvc5Authorize]
        [Activity(Description ="خروج از حساب کاربری")]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(MVC.Home.Index());
        }

        #endregion

        #region Validation

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual JsonResult IsEmailAvailable(string email)
        {
            return _userManager.IsEmailAvailableForConfirm(email) ? Json(true) : Json(false);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual JsonResult CheckPassword(string password)
        {
            return password.IsSafePasword() ? Json(true) : Json(false);
        }
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual JsonResult IsNameForShowExist(string nameForShow, Guid? id)
        {
            return _userManager.CheckNameForShowExist(nameForShow, id) ? Json(false) : Json(true);
        }
        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual JsonResult IsEmailExist(string email, Guid? id)
        {
            var check = _userManager.CheckEmailExist(email, id);
            return check ? Json(false) : Json(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual JsonResult IsUserNameExist(string userName, Guid? id)
        {
            return _userManager.CheckUserNameExist(userName, id) ? Json(false) : Json(true);
        }
        #endregion

        #region Private
        [NonAction]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(MVC.Home.Index());
        }
        
        #endregion
    }
}