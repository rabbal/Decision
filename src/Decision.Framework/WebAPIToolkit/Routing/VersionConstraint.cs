using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Decision.Framework.WebAPIToolkit.Routing
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        public VersionConstraint(string allowedVersion)
        {
            AllowedVersion = allowedVersion.ToLowerInvariant();
        }

        public string AllowedVersion { get; }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return AllowedVersion.Equals(value.ToString().ToLowerInvariant());
            }
            return false;
        }
    }
}