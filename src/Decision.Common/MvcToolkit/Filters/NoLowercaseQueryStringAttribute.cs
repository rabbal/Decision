using System;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Filters
{
    /// <summary>
    /// Ensures that a HTTP request URL can contain query string parameters with both upper-case and lower-case 
    /// characters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NoLowercaseQueryStringAttribute : FilterAttribute
    {
    }
}
