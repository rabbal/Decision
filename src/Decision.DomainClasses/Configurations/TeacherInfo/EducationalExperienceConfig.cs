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
    /// نشان دهنده مپینگ مربوط به کلاس سوابق تدرسی
    /// </summary>
    public class EducationalExperienceConfig : EntityTypeConfiguration<EducationalExperience>
   {
        /// <summary>
        /// 
        /// </summary>
        public EducationalExperienceConfig()
        {
            
            Property(t => t.RowVersion).IsRowVersion();

            HasRequired(t => t.Title)
              .WithMany(t => t.EducationalExperiences)
              .HasForeignKey(t => t.TitleId)
              .WillCascadeOnDelete(false);

            HasRequired(t => t.Creator).WithMany(u => u.CreatedEducationalExperiences).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(t => t.LasModifier).WithMany(u => u.ModifiedEducationalExperiences).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
