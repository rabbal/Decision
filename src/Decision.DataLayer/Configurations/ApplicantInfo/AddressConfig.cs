using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.ApplicantInfo;

namespace Decision.DataLayer.Configurations.ApplicantInfo
{
    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            Property(a => a.CellPhone).HasMaxLength(20).IsOptional();
            Property(a => a.PhoneNumber).HasMaxLength(20).IsOptional();
            Property(a => a.Location).IsMaxLength().IsRequired();
            Property(t => t.City).IsRequired().HasMaxLength(50);
            Property(t => t.State).IsRequired().HasMaxLength(50);
            Property(a => a.RowVersion).IsRowVersion();

            HasRequired(a=>a.Applicant).WithMany(j=>j.Addresses).HasForeignKey(a=>a.ApplicantId).WillCascadeOnDelete(true);
            HasRequired(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).WillCascadeOnDelete(false);
            HasRequired(e => e.ModifiedBy).WithMany().HasForeignKey(e => e.ModifiedById).WillCascadeOnDelete(false);
        }
    }
}
