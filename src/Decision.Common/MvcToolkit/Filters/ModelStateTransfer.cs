using System;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected static readonly string Key = typeof(ModelStateTransfer).FullName;
        
    }
}