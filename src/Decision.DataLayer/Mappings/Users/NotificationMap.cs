using System;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    public class NotificationMap : EntityMap<Notification,Guid>
    {
        public NotificationMap()
        {
            Property(a => a.Message).IsOptional().HasMaxLength(512);
            Property(a => a.Title).IsRequired().HasMaxLength(256);
            Property(a => a.ReceivedDateTime).IsRequired();
            Property(a => a.Type).IsRequired();
            Property(a => a.Url).IsOptional().HasMaxLength(1024);

            HasRequired(a => a.User).WithMany().HasForeignKey(a => a.UserId);
        }
    }
}
