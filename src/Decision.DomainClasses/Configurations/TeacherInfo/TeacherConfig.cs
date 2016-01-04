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
    /// نشان دهنده مپینگ مربوط به کلاس استاد
    /// </summary>
    public class TeacherConfig : EntityTypeConfiguration<Teacher>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public TeacherConfig()
        {
            Property(j => j.AccountNumber).HasMaxLength(256).IsOptional();
            Property(j => j.BankBranch).HasMaxLength(256).IsOptional();
            Property(j => j.BankName).HasMaxLength(256).IsRequired();
            Property(j => j.PersonnelCode).HasMaxLength(50).IsRequired();
            Property(j => j.Score).HasPrecision(7,2);
            Property(j => j.TrainingGPA).HasPrecision(7, 2);
            Property(j => j.BirthCertificateNumber).HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_TeacherBirthCertificateNumber")));
            Property(j => j.BirthPlaceCity).HasMaxLength(50).IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TeacherBirthPlaceCity")));
            Property(j => j.BirthPlaceState).HasMaxLength(50).IsOptional()
              .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TeacherBirthPlaceState"))); 
            Property(j => j.FirstName).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TeacherFirstName"))); 
            Property(j => j.LastName).HasMaxLength(50).IsRequired()
                  .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TeacherLastName") )); 
            Property(j => j.NationalCode).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_TeacherNationalCode") { IsUnique = true }));

            Property(j => j.RowVersion).IsRowVersion();

            HasOptional(j=>j.Position).WithMany(t=>t.Teachers).HasForeignKey(j=>j.PositionId).WillCascadeOnDelete(false);
            HasOptional(j=>j.TrainingCourse).WithMany(t=>t.Teachers).HasForeignKey(j=>j.TrainingCourseId).WillCascadeOnDelete(false);
            
           
            HasOptional(j => j.ApproveBy)
                .WithMany(u => u.ApprovedTeachers)
                .HasForeignKey(j => j.ApproveById)
                .WillCascadeOnDelete(false);


            HasMany(j => j.EducationalExperiences).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);
            HasMany(j=>j.EducationalBackgrounds).WithRequired(e=>e.Teacher).HasForeignKey(e=>e.TeacherId).WillCascadeOnDelete(true);
            HasMany(j => j.WorkExperiences).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);
            HasMany(j => j.ReseachExperiences).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);
            HasMany(j => j.TeacherInServiceCourseTypes).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);         

            HasMany(j => j.Articles).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId).WillCascadeOnDelete(true);
        
           
            HasRequired(e => e.Creator).WithMany(u => u.CreatedTeachers).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedTeachers).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);

        }
    }
}
