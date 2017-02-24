using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Users;

namespace Decision.DataLayer.Configurations.Users
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
