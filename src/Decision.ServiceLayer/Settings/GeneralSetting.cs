using Decision.DataLayer.Context;

namespace Decision.ServiceLayer.Settings
{
    public class GeneralSettings : SettingsBase
    {
        public GeneralSettings(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        /// <summary>
        /// get or set name of the site
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// get or set email of admin
        /// </summary>
        public string AdminEmail { get; set; }
        /// <summary>
        /// get or set facebook url
        /// </summary>
        public string FaceBookPageUrl { get; set; }
        /// <summary>
        /// get or set google+ url
        /// </summary>
        public string GooglePlusUrl { get; set; }
        /// <summary>
        /// get or set linkedIn url
        /// </summary>
        public string LinkedInPageUrl { get; set; }

    }
}
