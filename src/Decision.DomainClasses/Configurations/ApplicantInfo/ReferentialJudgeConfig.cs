using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Configurations.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس ارجاعات اساتید برای ویرایش
    /// </summary>
    public class ReferentialApplicantConfig : EntityTypeConfiguration<ReferentialApplicant>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ReferentialApplicantConfig()
        {
            HasRequired(r => r.ReferencedFrom)
                .WithMany(u => u.SentReferentialApplicants)
                .HasForeignKey(r => r.ReferencedFromId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.ReferencedTo)
                .WithMany(u => u.ReceivedReferentialApplicants)
                .HasForeignKey(r => r.ReferencedToId)
                .WillCascadeOnDelete(false);


            HasRequired(r => r.Applicant)
                .WithMany(u => u.ReferentialApplicants)
                .HasForeignKey(r => r.ApplicantId)
                .WillCascadeOnDelete(true);

            
        }
    }
}
