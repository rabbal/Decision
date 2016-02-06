using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Configurations.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده مپینگ  مربوط به کلاس مقاله متقاضی
    /// </summary>
    public class ArticleConfig : EntityTypeConfiguration<Article>
   {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ArticleConfig()
        {
            Property(j => j.Code).HasMaxLength(50).IsRequired();
            Property(j => j.Brief).IsMaxLength().IsOptional();
            Property(j => j.Content).IsMaxLength().IsRequired();
            Property(j => j.RowVersion).IsRowVersion();
            HasRequired(e => e.Creator).WithMany(u => u.CreatedArticles).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedArticles).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
