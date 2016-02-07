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
    /// نشان دهنده مپینگ مربوط به کلاس ارزیابی های متقاضی
    /// </summary>
    public class EntireEvaluationConfig : EntityTypeConfiguration<EntireEvaluation>
   {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public EntireEvaluationConfig()
        {
            Property(e=> e.Content).IsMaxLength().IsRequired();
            
            Property(e=> e.Foible).IsMaxLength().IsRequired();
            Property(e=> e.StrongPoint).IsMaxLength().IsRequired();
            Property(e=> e.RowVersion).IsRowVersion();

            HasRequired(e => e.Applicant).WithMany(e => e.EntireEvaluations).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
           
            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
