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

          
            HasRequired(t => t.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(t => t.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
