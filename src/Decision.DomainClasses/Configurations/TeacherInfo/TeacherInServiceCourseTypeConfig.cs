using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Configurations.TeacherInfo
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس " یک استاد در از یک نوع دوره ضمن خدمت چند ساعت گذرانده" میباشد
    /// </summary>
    public class TeacherInServiceCourseTypeConfig : EntityTypeConfiguration<TeacherInServiceCourseType>
   {
        /// <summary>
        /// 
        /// </summary>
        public TeacherInServiceCourseTypeConfig()
        {
            Property(j => j.RowVersion).IsRowVersion();
            HasRequired(j => j.InServiceCourseTypeTitle)
                .WithMany(t => t.TeacherInServiceCourseTypes)
                .HasForeignKey(j => j.InServiceCourseTypeTitleId)
                .WillCascadeOnDelete(false);

            HasRequired(w => w.Creator)
                .WithMany(u => u.CreatedTeacherInServiceCourseTypes)
                .HasForeignKey(e => e.CreatorId)
                .WillCascadeOnDelete(false);
            HasOptional(w => w.LasModifier)
                .WithMany(u => u.ModifiedTeacherInServiceCourseTypes)
                .HasForeignKey(e => e.LasModifierId)
                .WillCascadeOnDelete(false);
        }
    }
}
