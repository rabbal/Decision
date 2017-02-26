using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Common
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به تنظیمات
    /// </summary>
    public class SettingConfig : EntityTypeConfiguration<Setting>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public SettingConfig()
        {
            HasKey(a => new
            {
                a.Name
            });
            Property(s => s.Name).HasMaxLength(50).IsRequired();
        }
    }
}
