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
    /// نشان دهنده مپینگ مربوط به کلاس موسسه
    /// </summary>
    public class InstitutionConfig : EntityTypeConfiguration<Institution>
   {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public InstitutionConfig()
        {
            Property(i => i.Name).HasMaxLength(256).IsRequired();
            Property(i => i.Description).HasMaxLength(1024).IsRequired();

            Property(i => i.RowVersion).IsRowVersion();

            HasRequired(e => e.Creator).WithMany(u => u.CreatedInstitutions).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedInstitutions).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
