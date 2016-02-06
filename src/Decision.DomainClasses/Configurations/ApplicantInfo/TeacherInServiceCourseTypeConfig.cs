using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Configurations.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس " یک متقاضی در از یک نوع دوره ضمن خدمت چند ساعت گذرانده" میباشد
    /// </summary>
    public class ApplicantInServiceCourseTypeConfig : EntityTypeConfiguration<ApplicantInServiceCourseType>
   {
        /// <summary>
        /// 
        /// </summary>
        public ApplicantInServiceCourseTypeConfig()
        {
            Property(j => j.RowVersion).IsRowVersion();
            HasRequired(j => j.InServiceCourseTypeTitle)
                .WithMany(t => t.ApplicantInServiceCourseTypes)
                .HasForeignKey(j => j.InServiceCourseTypeTitleId)
                .WillCascadeOnDelete(false);

            HasRequired(w => w.Creator)
                .WithMany(u => u.CreatedApplicantInServiceCourseTypes)
                .HasForeignKey(e => e.CreatorId)
                .WillCascadeOnDelete(false);
            HasOptional(w => w.LasModifier)
                .WithMany(u => u.ModifiedApplicantInServiceCourseTypes)
                .HasForeignKey(e => e.LasModifierId)
                .WillCascadeOnDelete(false);
        }
    }
}
