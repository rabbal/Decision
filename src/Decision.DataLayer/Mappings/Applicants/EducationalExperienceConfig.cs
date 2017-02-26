using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class EducationalExperienceConfig : EntityTypeConfiguration<EducationalExperience>
   {
        public EducationalExperienceConfig()
        {
            
            Property(t => t.RowVersion).IsRowVersion();

          
            HasRequired(t => t.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(t => t.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
