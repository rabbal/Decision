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
    /// نشان دهنده مپینگ مربوط به کلاس سوابق پژوهشی
    /// </summary>
    public class ReseachExperienceConfig : EntityTypeConfiguration<ResearchExperience>
  {
        /// <summary>
        /// 
        /// </summary>
        public ReseachExperienceConfig()
        {
            Property(r => r.Title).IsMaxLength().IsRequired();
            Property(r => r.Description).IsMaxLength().IsOptional();
            Property(r => r.RowVersion).IsRowVersion();


            HasRequired(e => e.Creator).WithMany(u => u.CreatedReseachExperiences).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedReseachExperiences).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
