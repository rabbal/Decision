using System;
using System.Web;

namespace Decision.Common.Fakes
{
    public static class Extentions
    {
        /// <summary>
        ///     Indicates whether this context is fake
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <returns>Result</returns>
        public static bool IsFakeContext(this HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            return httpContext is FakeHttpContext;
        }
    }
}