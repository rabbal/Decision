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
    /// نشان دهنده مپینگ مربوط به کلاس گزینه های سوالات
    /// </summary>
    public class AnswerOptionConfig : EntityTypeConfiguration<AnswerOption>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AnswerOptionConfig()
        {
            Property(a => a.Name).HasMaxLength(1024).IsRequired();
            

            HasMany(a => a.ArticleEvaluations)
                .WithMany(je => je.AnswerOptions)
                .Map(
                    a =>
                        a.ToTable("AnswerOptionApplicantmentEvaluation")
                            .MapRightKey("ArticleEvaluationId")
                            .MapLeftKey("AnswerOptionId"));

           
        }
    }
}
