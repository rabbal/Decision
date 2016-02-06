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


            HasRequired(w => w.Title)
                .WithMany(a => a.WorkExperiences)
                .HasForeignKey(w => w.TitleId)
                .WillCascadeOnDelete(false);

            HasRequired(w =>w.Creator).WithMany(u => u.CreatedWorkExperiences).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(w => w.LasModifier).WithMany(u => u.ModifiedWorkExperiences).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
