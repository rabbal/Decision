using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Evaluations;

namespace Decision.DomainClasses.Configurations.Evaluations
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس ارزش گذار
    /// </summary>
    public class AppraiserConfig : EntityTypeConfiguration<Appraiser>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AppraiserConfig()
        {
            Property(a => a.FirstName).HasMaxLength(256).IsRequired();
            Property(a => a.LastName).HasMaxLength(256).IsRequired();
            Property(a => a.CellPhone).HasMaxLength(20).IsRequired();
            Property(a => a.RowVersion).IsRowVersion();

            HasRequired(a=>a.AppraiserTitle).WithMany(t=>t.Appraisers).HasForeignKey(a=>a.AppraiserTitleId).WillCascadeOnDelete(false);

            HasMany(a => a.EntireEvaluations).WithRequired(e => e.Evaluator).HasForeignKey(e => e.EvaluatorId).WillCascadeOnDelete(false);
            HasMany(a => a.ArticlesEvaluations).WithRequired(e => e.Evaluator).HasForeignKey(e => e.EvaluatorId).WillCascadeOnDelete(false);
            HasMany(a => a.Interviews).WithRequired(e => e.Interviewer).HasForeignKey(e => e.InterviewerId).WillCascadeOnDelete(false);

            HasRequired(c => c.Creator).WithMany(u => u.CreatedAppraisers).HasForeignKey(c => c.CreatorId).WillCascadeOnDelete(false);
            HasOptional(c => c.LasModifier).WithMany(u => u.ModifiedAppraisers).HasForeignKey(c => c.LasModifierId).WillCascadeOnDelete(false);

        }
    }
}
