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
    /// نشان دهنده مپینگ مربوط به کلاس مراکز کار آموزی
    /// </summary>
    public class TrainingCenterConfig : EntityTypeConfiguration<TrainingCenter>
    {
        /// <summary>
        /// 
        /// </summary>
        public TrainingCenterConfig()
        {
            Property(t => t.CenterName).HasMaxLength(256).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueCenterName") { IsUnique = true, Order = 1 }));
            Property(t=>t.City).IsRequired().HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_UniqueCenterName") { IsUnique = true, Order = 2 }));

            Property(t => t.State).IsRequired().HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TrainingCenterState")));

            Property(t => t.RowVersion).IsRowVersion();
            Property(t => t.Location).IsMaxLength().IsOptional();
            Property(t => t.PhoneNumber1).HasMaxLength(20).IsOptional();
            Property(t => t.PhoneNumber2).HasMaxLength(20).IsOptional();
            Property(t => t.Description).HasMaxLength(1024).IsOptional();
            HasMany(t => t.Courses).WithRequired(a => a.TrainingCenter).HasForeignKey(a => a.TrainingCenterId).WillCascadeOnDelete(true);

           

            HasRequired(t => t.Creator).WithMany(u => u.CreatedTrainingCenters).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(t => t.LasModifier).WithMany(u => u.ModifiedTrainingCenters).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
