namespace Decision.Common.WebAPIToolkit.Routing
{
    /// <summary>
    /// usage [ApiVersion1RoutePrefix("product")]
    /// </summary>
    public sealed class Version1RoutePrefixAttribute : RoutePrefixAttribute
    {
        private const string RouteBase = "api/{apiVersion:VersionConstraint(v1)}";
        private const string PrefixRouteBase = RouteBase + "/";

        public Version1RoutePrefixAttribute(string routePrefix)
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
        {
        }
    }
}