namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public static class HttpPropertyKeys
    {
        /// Provides a key for the HttpConfiguration associated with this ///request.
        public static readonly string HttpConfigurationKey = "MS_HttpConfiguration";

        /// Provides a key for the IHttpRouteData associated with this request.
        public static readonly string HttpRouteDataKey = "MS_HttpRouteData";

        /// Provides a key for the HttpActionDescriptor associated with this ///request.
        public static readonly string HttpActionDescriptorKey = "MS_HttpActionDescriptor";

        /// Provides a key for the current SynchronizationContext stored in ///HttpRequestMessage.Properties
        /// If SynchronizationContext.Current is null then no context is ///stored.
        public static readonly string SynchronizationContextKey = "MS_SynchronizationContext";

        /// Provides a key for the collection of resources that should be ///disposed when a request is disposed.
        public static readonly string DisposableRequestResourcesKey = "MS_DisposableRequestResources";

        /// Provides a key for the dependency scope for this request.
        public static readonly string DependencyScope = "MS_DependencyScope";

        /// Provides a key for the client certificate for this request.
        public static readonly string ClientCertificateKey = "MS_ClientCertificate";

        /// Provides a key for a delegate which can retrieve the client ///certificate for this request.
        public static readonly string RetrieveClientCertificateDelegateKey = "MS_RetrieveClientCertificateDelegate";

        /// Provides a key for the HttpRequestContext for this request.
        public static readonly string RequestContextKey = "MS_RequestContext";

        /// <summary>
        ///     Provides a key for the Guid stored in ///HttpRequestMessage.Properties.
        ///     This is the correlation id for that request.
        /// </summary>
        public static readonly string RequestCorrelationKey = "MS_RequestId";

        /// Provides a key that indicates whether the request originates from a ///local address.
        public static readonly string IsLocalKey = "MS_IsLocal";

        /// Provides a key that indicates whether the request failed to match a ///route.
        public static readonly string NoRouteMatched = "MS_NoRouteMatched";

        /// Provides a key that indicates whether error details are to be ///included in the response for this HTTP request.
        public static readonly string IncludeErrorDetailKey = "MS_IncludeErrorDetail";

        /// Provides a key for the parsed query string stored in ///HttpRequestMessage.Properties.
        public static readonly string RequestQueryNameValuePairsKey = "MS_QueryNameValuePairs";

        /// Provides a key that indicates whether the request is a batch ///request.
        public static readonly string IsBatchRequest = "MS_BatchRequest";
    }
}