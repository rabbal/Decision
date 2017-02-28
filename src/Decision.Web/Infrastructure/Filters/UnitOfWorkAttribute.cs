using System;
using System.Web.Mvc;
using Decision.DataLayer.Context;
using Decision.Framework.Domain.Uow;

namespace Decision.Web.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public IUnitOfWork UnitOfWork { get; set; }

        // after action executed
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (filterContext.Exception == null)
            {
                //try
                //{
                //    UnitOfWork.SaveChanges();
                //}
                //catch (DbEntityValidationException validationException)
                //{
                //}
                //catch (DbUpdateConcurrencyException concurrencyException)
                //{
                //}
                //catch (DbUpdateException updateException)
                //{
                //}
            }
        }
    }
}