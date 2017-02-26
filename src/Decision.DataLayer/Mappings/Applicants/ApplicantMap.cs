using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Decision.DomainClasses.Applicants;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class ApplicantMap : TrackableEntityMap<Applicant, long>
    {
        public ApplicantMap()
        {
            Property(a => a.BirthCertificateNumber).HasMaxLength(20)
                .IsRequired();

            Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            Property(a => a.LastName).HasMaxLength(50).IsRequired();

            Property(a => a.NationalCode).HasMaxLength(50).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UIX_Applicant_NationalCode") { IsUnique = true }));

            HasMany(a => a.EducationalExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.EducationalBackgrounds).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.WorkExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.ReseachExperiences).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.Articles).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.EntireEvaluations).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
            HasMany(a => a.Interviews).WithRequired(e => e.Applicant).HasForeignKey(e => e.ApplicantId).WillCascadeOnDelete(true);
        }
    }
}
