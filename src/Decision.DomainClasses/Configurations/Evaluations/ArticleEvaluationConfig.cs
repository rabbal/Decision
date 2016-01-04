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
    /// نشان دهنده مپینگ مربوط به کلاس ارزیابی از مقالات
    /// </summary>
    public class ArticleEvaluationConfig : EntityTypeConfiguration<ArticleEvaluation>
   {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ArticleEvaluationConfig()
        {
            Property(je => je.Brief).IsMaxLength().IsOptional();
            Property(je => je.Content).IsMaxLength().IsOptional();
            Property(je => je.Foible).IsMaxLength().IsOptional();
            Property(je => je.StrongPoint).IsMaxLength().IsOptional();
            Property(je => je.RowVersion).IsRowVersion();
            
            HasRequired(je=>je.Evaluator).WithMany(e=>e.ArticlesEvaluations).HasForeignKey(je=>je.EvaluatorId).WillCascadeOnDelete(false);
            HasRequired(je => je.Article).WithMany(e => e.ArticleEvaluations).HasForeignKey(je => je.ArticleId).WillCascadeOnDelete(true);

            HasRequired(e => e.Creator).WithMany(u => u.CreatedArticleEvaluations).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedArticleEvaluations).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);

        }
    }
}
