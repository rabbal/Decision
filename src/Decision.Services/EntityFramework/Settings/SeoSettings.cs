using Decision.DataLayer.Context;

namespace Decision.ServiceLayer.EntityFramework.Settings
{
    public class SeoSettings : SettingsBase
    {
        public SeoSettings(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
