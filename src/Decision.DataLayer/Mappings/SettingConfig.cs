using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses;

namespace Decision.DataLayer.Mappings
{
    public class SettingConfig : EntityMap<Setting,long>
    {
        public SettingConfig()
        {
            Property(s => s.Name).HasMaxLength(50).IsRequired();
        }
    }
}
