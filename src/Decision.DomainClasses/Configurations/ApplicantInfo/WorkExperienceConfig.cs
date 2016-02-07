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
    /// نشان دهنده میپنگ مربوط به کلاس سوابق کاری
    /// </summary>
    public class WorkExperienceConfig : EntityTypeConfiguration<WorkExperience>
  {
        /// <summary>
        /// 
        /// </summary>
        public WorkExperienceConfig()
        {
            Property(w => w.OfficeName).HasMaxLength(1024).IsRequired();
            Property(w => w.RowVersion).IsRowVersion();
            HasRequired(w =>w.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(w => w.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
