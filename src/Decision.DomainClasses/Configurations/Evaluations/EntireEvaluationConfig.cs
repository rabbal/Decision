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
    /// نشان دهنده مپینگ مربوط به کلاس ارزیابی های استاد
    /// </summary>
    public class EntireEvaluationConfig : EntityTypeConfiguration<EntireEvaluation>
   {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public EntireEvaluationConfig()
        {
            Property(e=> e.Content).IsMaxLength().IsRequired();
            Property(e=> e.Brief).IsMaxLength().IsRequired();
            Property(e=> e.Foible).IsMaxLength().IsRequired();
            Property(e=> e.StrongPoint).IsMaxLength().IsRequired();
            Property(e=> e.RowVersion).IsRowVersion();

            HasRequired(e => e.Teacher).WithMany(e => e.EntireEvaluations).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);
            HasRequired(e=>e.Evaluator).WithMany(e=>e.EntireEvaluations).HasForeignKey(e=>e.EvaluatorId).WillCascadeOnDelete(false);

            HasRequired(e => e.Creator).WithMany(u => u.CreatedEntireEvaluations).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedEntireEvaluations).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
