using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Evaluations;

namespace Decision.DomainClasses.Configurations.Evaluations
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
            Property(i => i.Brief).IsMaxLength().IsRequired();
            Property(i => i.RowVersion).IsRowVersion();
            HasRequired(i=>i.Interviewer).WithMany(e=>e.Interviews).HasForeignKey(i=>i.InterviewerId).WillCascadeOnDelete(false);
            HasRequired(i => i.Applicant).WithMany(e => e.Interviews).HasForeignKey(i => i.ApplicantId).WillCascadeOnDelete(true);

            HasRequired(e => e.Creator).WithMany(u => u.CreatedInterviews).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedInterviews).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
