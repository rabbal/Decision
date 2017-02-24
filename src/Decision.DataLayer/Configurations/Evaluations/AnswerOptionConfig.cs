using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.Evaluations
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس گزینه های سوالات
    /// </summary>
    public class AnswerOptionConfig : EntityTypeConfiguration<AnswerOption>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AnswerOptionConfig()
        {
            Property(a => a.Title).HasMaxLength(1024).IsRequired();
            Property(q => q.RowVersion).IsRowVersion();
            HasRequired(a => a.CreatedBy).WithMany().HasForeignKey(a => a.CreatedById).WillCascadeOnDelete(false);
            HasRequired(a => a.ModifiedBy).WithMany().HasForeignKey(a => a.ModifiedById).WillCascadeOnDelete(false);

        }
    }
}
