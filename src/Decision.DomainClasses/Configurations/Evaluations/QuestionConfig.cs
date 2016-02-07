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
    /// نشان دهنده مپینگ مربوط به کلاس سوال
    /// </summary>
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public QuestionConfig()
        {
            Property(q => q.Title).IsMaxLength().IsRequired();
            Property(q => q.RowVersion).IsRowVersion();
            
            HasMany(q => q.AnswerOptions)
                .WithRequired(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .WillCascadeOnDelete(true);

            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
