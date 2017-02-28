using System;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    public class ActivityLogMap : EntityMap<ActivityLog, long>
    {
        public ActivityLogMap()
        {
            Property(a => a.Description).HasMaxLength(512).IsOptional();
            Property(a => a.Title).IsRequired().HasMaxLength(256);
            Property(a => a.Url).HasMaxLength(1024).IsOptional();
            Property(a => a.LogDateTime).IsRequired();

            HasRequired(a => a.User).WithMany().HasForeignKey(a => a.UserId);
        }
    }
}
