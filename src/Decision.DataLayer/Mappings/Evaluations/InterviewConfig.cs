using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Evaluations
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس مصاحبه های متقاضی
    /// </summary>
    public class InterviewConfig : EntityTypeConfiguration<Interview>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public InterviewConfig()
        {
            Property(i => i.Body).IsMaxLength().IsRequired();
            Property(i => i.RowVersion).IsRowVersion();
            HasRequired(i => i.Applicant).WithMany(e => e.Interviews).HasForeignKey(i => i.ApplicantId).WillCascadeOnDelete(true);

            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
