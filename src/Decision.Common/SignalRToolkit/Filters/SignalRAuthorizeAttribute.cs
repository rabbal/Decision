namespace Decision.Common.SignalRToolkit.Filters
{
    public sealed class SignalRAuthorizeAttribute : Microsoft.AspNet.SignalR.AuthorizeAttribute
    {
        public SignalRAuthorizeAttribute(params string[] permissions)
        {
            Roles = string.Join(",", permissions);
        }

    }
}
