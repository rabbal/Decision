using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Users
{
    public class ActivityLogConfiguration : EntityTypeConfiguration<ActivityLog>
    {
        public ActivityLogConfiguration()
        {
            Property(a => a.Description).HasMaxLength(512).IsOptional();
            Property(a => a.Title).IsRequired().HasMaxLength(256);
            Property(a => a.OperantIp).IsRequired().HasMaxLength(20);
            Property(a => a.Url).HasMaxLength(1024).IsOptional();
            Property(a => a.OperatedOn).IsRequired();
        }
    }
}
