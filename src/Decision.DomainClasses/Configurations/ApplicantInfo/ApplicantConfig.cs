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
    /// نشان دهنده مپینگ مربوط به کلاس متقاضی
    /// </summary>
    public class ApplicantConfig : EntityTypeConfiguration<Applicant>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ApplicantConfig()
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
                    new IndexAnnotation(new IndexAttribute("IX_ApplicantBirthCertificateNumber")));
            Property(j => j.BirthPlaceCity).HasMaxLength(50).IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantBirthPlaceCity")));
            Property(j => j.BirthPlaceState).HasMaxLength(50).IsOptional()
              .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantBirthPlaceState"))); 
            Property(j => j.FirstName).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantFirstName"))); 
            Property(j => j.LastName).HasMaxLength(50).IsRequired()
                  .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantLastName") )); 
            Property(j => j.NationalCode).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantNationalCode") { IsUnique = true }));

            Property(j => j.RowVersion).IsRowVersion();

            HasOptional(j=>j.Position).WithMany(t=>t.Applicants).HasForeignKey(j=>j.PositionId).WillCascadeOnDelete(false);
            HasOptional(j=>j.TrainingCourse).WithMany(t=>t.Applicants).HasForeignKey(j=>j.TrainingCourseId).WillCascadeOnDelete(false);
            
           
            HasOptional(j => j.ApproveBy)
                .WithMany(u => u.ApprovedApplicants)
                .HasForeignKey(j => j.ApproveById)
                .WillCascadeOnDelete(false);


            HasMany(j => j.EducationalExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j=>j.EducationalBackgrounds).WithRequired(e=>e.Applicant).HasForeignKey(e=>e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j => j.WorkExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j => j.ReseachExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j => j.ApplicantInServiceCourseTypes).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);         

            HasMany(j => j.Articles).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
        
           
            HasRequired(e => e.Creator).WithMany(u => u.CreatedApplicants).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedApplicants).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);

        }
    }
}
