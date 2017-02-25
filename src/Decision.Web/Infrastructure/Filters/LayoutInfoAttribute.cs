using System;
using System.Web.Mvc;
using Decision.Framework.Configuration;
using Decision.Models.Web;
using Decision.Services.Interfaces.Users;

namespace Decision.Web.Infrastructure.Filters
{

    public sealed class LayoutInfoAttribute : ActionFilterAttribute
    {
        public Func<IAppConfiguration> AppConfiguration { get; set; }
        public Func<ICurrentUser> CurrentUser { get; set; }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (CurrentUser == null) throw new  NullReferenceException($" in layoutinfo attribute:{nameof(CurrentUser)}");

            if (AppConfiguration == null) throw new NullReferenceException($" in layoutinfo attribute:{nameof(AppConfiguration)}");

            if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest()) return;

            var viewBag = filterContext.Controller.ViewBag;
            var currentUser = CurrentUser();

            var authentication = new AuthenticationViewModel
            {
                IsAuthenticated = currentUser.IsAuthenticated
            };

            if (currentUser.IsAuthenticated)
            {
                authentication.UserName = currentUser.UserName;
                authentication.DisplayName = currentUser.DisplayName;
                authentication.UserId = currentUser.Id;
            }

            var configuration = AppConfiguration();
            viewBag.Authentication = authentication;
            viewBag.SiteShortName = configuration.SiteShortName;
            viewBag.SiteName = configuration.SiteName;
            //todo:add other things You need in layout


            base.OnActionExecuted(filterContext);
        }

    }


}