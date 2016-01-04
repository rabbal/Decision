using Decision.DataLayer.Context;

namespace Decision.ServiceLayer.Settings
{
    public class ExternalAuthSettings : SettingsBase
    {
        public ExternalAuthSettings(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            AutoRegisterEnabled = true;
            GoogleSystemEnable = true;
            FacebookSystemEnable = true;
        }

        public bool AutoRegisterEnabled { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleCientSecret { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        public bool FacebookSystemEnable { get; set; }
        public bool GoogleSystemEnable { get; set; }
    }
}
