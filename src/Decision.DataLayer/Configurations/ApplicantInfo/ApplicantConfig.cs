using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Configurations.ApplicantInfo
{
    public class ApplicantConfig : EntityTypeConfiguration<Applicant>
    {
       
        public ApplicantConfig()
        {
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
            

            HasMany(j => j.EducationalExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j=>j.EducationalBackgrounds).WithRequired(e=>e.Applicant).HasForeignKey(e=>e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j => j.WorkExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(j => j.ReseachExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
           
            HasMany(j => j.Articles).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
        
           
            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);

        }
    }
}
