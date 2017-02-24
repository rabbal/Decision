namespace Decision.DomainClasses.Common
{
    /// <summary>
    ///     نشان دهنده انواع لاگ های سیستم میباشد
    /// </summary>
    public enum AuditLogType
    {
        Serialize,
        JustDescription,
        Login,
        LogOut
    }
}