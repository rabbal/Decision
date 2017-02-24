using System;
using System.Web.Mvc;
using Decision.Services.Interfaces.Users;

namespace Decision.Web.Infrastructure.Filters
{
    public sealed class UpdateLastActivityAttribute : FilterAttribute, IResultFilter
    {
        public Func<IUserService> UserService { get; set; }
        public Func<ICurrentUser> CurrentUser { get; set; }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Do nothing, just sleep.
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException(nameof(filterContext));
            if (CurrentUser == null)
                throw new NullReferenceException($"{nameof(CurrentUser)} In UpdateUserLastActivityAttribute is null");
            if (CurrentUser == null)
                throw new NullReferenceException($"{nameof(UserService)} In UpdateUserLastActivityAttribute is null");

            var currentUser = CurrentUser.Invoke();

            if (currentUser.IsAuthenticated)
            {
                UserService.Invoke().UpdateLastActivity();
            }
        }
    }
}