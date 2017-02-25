using System;
using System.Web.Mvc;
using System.Web.UI;

namespace Decision.Framework.MvcToolkit.Filters
{
    /// <summary>
    /// Represents an attribute that is used to mark an action method whose output will not be cached.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoOutputCacheAttribute : OutputCacheAttribute
    {
        public NoOutputCacheAttribute()
        {
            NoStore = true;
            Location = OutputCacheLocation.None;
            // Duration = 0 by default.
            // VaryByParam = "*" by default.
        }
    }
}
