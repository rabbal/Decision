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
    /// نشان دهنده مپینگ مروبط کلاس ( در یک ارزیابی به یک سوال با جواب مقداری چه مقداری داده شده است) مباشد
    /// </summary>
    public class ArticleEvaluationQuestionConfig : EntityTypeConfiguration<ArticleEvaluationQuestion>
    {
        /// <summary>
        /// سازنده پیشفرض
        /// </summary>
        public ArticleEvaluationQuestionConfig()
        {
            Property(jeq => jeq.Value).IsMaxLength().IsRequired();

            Property(jeq => jeq.QuestionId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ ArticleEvaluationQuestion") { IsUnique = true ,Order = 1}));
            Property(jeq => jeq.ArticleEvaluationId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ ArticleEvaluationQuestion") { IsUnique = true, Order = 2}));
            

            Property(jeq => jeq.RowVersion).IsRowVersion();

            HasRequired(jeq => jeq.Question)
                .WithMany(q => q.ArticleEvaluationQuestions)
                .HasForeignKey(jeq => jeq.QuestionId)
                .WillCascadeOnDelete(true);

            HasRequired(jeq => jeq.ArticleEvaluation)
              .WithMany(q => q.ArticleEvaluationQuestions)
              .HasForeignKey(jeq => jeq.ArticleEvaluationId)
              .WillCascadeOnDelete(true);

            

            HasRequired(e => e.Creator).WithMany(u => u.CreatedArticleEvaluationQuestions).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedArticleEvaluationQuestions).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);

        }
    }
}
