using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class EducationalBackgroundConfig : EntityTypeConfiguration<EducationalBackground>
   {
        public EducationalBackgroundConfig()
        {

            Property(e => e.Description).IsMaxLength().IsOptional();
            Property(e => e.Supervisor).HasMaxLength(50).IsOptional();
            Property(e => e.Advisor).HasMaxLength(50).IsOptional();
            Property(e => e.ThesisTopic).IsMaxLength().IsRequired();
            Property(e => e.RowVersion).IsRowVersion();
            Property(e => e.GPA).IsRequired().HasPrecision(7, 2);
            Property(e => e.ThesisScore).IsRequired().HasPrecision(7, 2);

            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
