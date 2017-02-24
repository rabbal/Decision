using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.ApplicantInfo
{
    public class WorkExperienceConfig : EntityTypeConfiguration<WorkExperience>
  {
      
        public WorkExperienceConfig()
        {
            Property(w => w.OfficeName).HasMaxLength(1024).IsRequired();
            Property(w => w.RowVersion).IsRowVersion();
            HasRequired(w =>w.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(w => w.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
