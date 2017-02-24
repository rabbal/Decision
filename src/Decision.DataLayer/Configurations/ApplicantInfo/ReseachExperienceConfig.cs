using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.ApplicantInfo
{
    public class ReseachExperienceConfig : EntityTypeConfiguration<ResearchExperience>
  {
        public ReseachExperienceConfig()
        {
                        Property(r => r.RowVersion).IsRowVersion();


            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
