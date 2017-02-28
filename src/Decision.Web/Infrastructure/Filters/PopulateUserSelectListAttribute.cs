using System;
using System.Web.Mvc;
using Decision.Framework.Infrastructure;
using Decision.ServiceLayer.Interfaces.Identity;

namespace Decision.Web.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class PopulateUserSelectListAttribute : ActionFilterAttribute
    {
        #region Properties

        public IUserService UserService { get; set; }

        #endregion

        #region Public Methods 

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException(nameof(filterContext));

            var viewResult = filterContext.Result as ViewResult;
            if (viewResult?.Model is IHaveUserSelectList)
            {
                ((IHaveUserSelectList) viewResult.Model).AvailableUsers = UserService.GetAvailableUsers();
            }
        }

        #endregion
    }
}