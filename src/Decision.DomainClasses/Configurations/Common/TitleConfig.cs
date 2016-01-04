using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Configurations.Common
{
    
    /// <summary>
    /// نشان دهندۀ  مپینگ مربوط به کلاس عنوان
    /// </summary>
    public class TitleConfig : EntityTypeConfiguration<Title>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public TitleConfig()
        {
            Property(t => t.Name).HasMaxLength(256).IsRequired()
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueTitleName") { IsUnique = true ,Order = 1}));
            Property(t => t.Type).IsRequired()
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueTitleName") { IsUnique = true,Order = 2}));
            Property(t => t.Category)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueTitleName") { IsUnique = true ,Order = 3}));

            Property(c => c.RowVersion).IsRowVersion();
            HasRequired(c => c.Creator).WithMany(u => u.CreatedTitles).HasForeignKey(c => c.CreatorId).WillCascadeOnDelete(false);
            HasOptional(c => c.LasModifier).WithMany(u => u.ModifiedTitles).HasForeignKey(c => c.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
