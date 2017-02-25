using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Decision.Framework.GuardToolkit;
using Decision.Framework.ReflectionToolkit;

namespace Decision.Framework.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class HandleConcurrencyExceptionFilter : FilterAttribute, IExceptionFilter
    {
        #region Fields

        private readonly string _rowVersionPropertyName;

        #endregion

        #region Public Methods

        public void OnException(ExceptionContext filterContext)
        {
            Check.ArgumentNotNull(filterContext, nameof(filterContext));

            if (filterContext.ExceptionHandled) return;

            var concurrencyException = filterContext.Exception as DbUpdateConcurrencyException;
            if (concurrencyException == null) return;

            var entry = concurrencyException.Entries.Single();
            var databaseEntry = entry.GetDatabaseValues();
            
            var entityStateIsDeleted = entry.State == EntityState.Deleted;
            string message;

            if (databaseEntry == null)
            {
                message = entityStateIsDeleted
                    ? Resources.RecordDeletedByAnotherUserOnDeleting
                    : Resources.RecordDeletedByAnotherUserOnEditing;

                filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty,
                    message);

                //todo: log

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult { ViewData = filterContext.Controller.ViewData };
                return;
            }

            message = entityStateIsDeleted
                   ? Resources.RecordModifiedByAnotherUserOnDeleting
                   : Resources.RecordModifiedByAnotherUserOnEditing;

            filterContext.Controller.ViewData.ModelState.AddModelError(string.Empty,
                message);

            //todo:log

            var databaseValues = databaseEntry.ToObject();
            filterContext.ExceptionHandled = true;
            UpdateTimestampProperty(filterContext, databaseValues);
            filterContext.Result = new ViewResult { ViewData = filterContext.Controller.ViewData };

        }

        #endregion

        #region Private Methods

        private void UpdateTimestampProperty(ControllerContext filterContext, object databaseValues)
        {
            Check.ArgumentNotNull(databaseValues, nameof(databaseValues));

            var propertyReflector = new PropertyReflector();

            var timestampValue = (byte[])propertyReflector.GetValue(databaseValues, _rowVersionPropertyName);

            filterContext.Controller.ViewData.ModelState.Add(_rowVersionPropertyName, new ModelState());
            filterContext.Controller.ViewData.ModelState.SetModelValue(_rowVersionPropertyName,
                new ValueProviderResult(Convert.ToBase64String(timestampValue),
                    Convert.ToBase64String(timestampValue), null));
        }


        #endregion

        #region Constructors

        public HandleConcurrencyExceptionFilter()
        {
            _rowVersionPropertyName = "RowVersion";
        }

        public HandleConcurrencyExceptionFilter(string rowVersionPropertyName)
        {
            _rowVersionPropertyName = rowVersionPropertyName;
        }
        #endregion
    }
}