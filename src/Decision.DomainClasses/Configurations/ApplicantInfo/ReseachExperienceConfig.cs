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
                        Property(r => r.RowVersion).IsRowVersion();


            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
