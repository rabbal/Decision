using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.ApplicantInfo
{
    public class ArticleConfig : EntityTypeConfiguration<Article>
   {
        public ArticleConfig()
        {
            
            Property(j => j.Brief).IsMaxLength().IsOptional();
            Property(j => j.RowVersion).IsRowVersion();
            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
