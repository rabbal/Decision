using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Applicants;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class ApplicantMap : TrackableEntityMap<Applicant, long>
    {
        public ApplicantMap()
        {
            Property(a => a.BirthCertificateNumber).HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_ApplicantBirthCertificateNumber")));
            Property(a => a.BirthPlaceCity).HasMaxLength(50).IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Applicant_ApplicantBirthPlaceCity")));
            Property(a => a.BirthPlaceState).HasMaxLength(50).IsOptional()
              .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantBirthPlaceState")));
            Property(a => a.FirstName).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantFirstName")));
            Property(a => a.LastName).HasMaxLength(50).IsRequired()
                  .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantLastName")));
            Property(a => a.NationalCode).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_ApplicantNationalCode") { IsUnique = true }));
           

            HasMany(a => a.EducationalExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.EducationalBackgrounds).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.WorkExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.ReseachExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);

            HasMany(a => a.Articles).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);

        }
    }
}
