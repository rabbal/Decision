using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Users
{
    public class UserSettings : ISettings
    {
        #region Properties
        public bool RegisterEnabled { get; set; }
        public bool LoginEnabled { get; set; }
        #endregion
    }
}