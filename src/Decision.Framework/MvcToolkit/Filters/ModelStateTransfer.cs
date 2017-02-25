using System;
using System.Web.Mvc;

namespace Decision.Framework.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
        
    }
}