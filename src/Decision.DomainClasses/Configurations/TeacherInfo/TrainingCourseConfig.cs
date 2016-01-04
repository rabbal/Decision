using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Configurations.TeacherInfo
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس دوره کار آموزی
    /// </summary>
    public class TrainingCourseConfig : EntityTypeConfiguration<TrainingCourse>
  {
        /// <summary>
        /// 
        /// </summary>
        public TrainingCourseConfig()
        {
            Property(t => t.CourseCode).HasMaxLength(256).IsRequired()
                 .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueCourseCode") { IsUnique = true, Order = 1 }));
            Property(t=>t.TrainingCenterId)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueCourseCode") { IsUnique = true, Order = 2 }));

            Property(t => t.RowVersion).IsRowVersion();
          
            HasRequired(t => t.Creator).WithMany(u => u.CreatedTrainingCourses).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(t => t.LasModifier).WithMany(u => u.ModifiedTrainingCourses).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
