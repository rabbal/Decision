using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Users
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            Property(a => a.Message).IsOptional().HasMaxLength(512);
            Property(a => a.Title).IsRequired().HasMaxLength(256);
            Property(a => a.ReceivedOn).IsRequired();
            Property(a => a.Type).IsRequired();
            Property(a => a.Url).IsOptional().HasMaxLength(1024);
        }
    }
}
