using System.Collections.Specialized;
using System.Configuration;

namespace Decision.Framework.Configuration
{
    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        public NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public string ConnectionStrings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public T GetSection<T>(string sectionName)
        {
            return (T) ConfigurationManager.GetSection(sectionName);
        }
    }
}


