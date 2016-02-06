using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Configurations.ApplicantInfo
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس آدرس
    /// </summary>
    public class AddressConfig : EntityTypeConfiguration<Address>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AddressConfig()
        {
            Property(a => a.CellPhone).HasMaxLength(20).IsOptional();
            Property(a => a.PhoneNumber).HasMaxLength(20).IsOptional();
            Property(a => a.Location).IsMaxLength().IsRequired();
            Property(t => t.City).IsRequired().HasMaxLength(50);
            Property(t => t.State).IsRequired().HasMaxLength(50);
            Property(a => a.RowVersion).IsRowVersion();

            HasRequired(a=>a.Applicant).WithMany(j=>j.Addresses).HasForeignKey(a=>a.ApplicantId).WillCascadeOnDelete(true);
            HasRequired(e => e.Creator).WithMany(u => u.CreatedAddresses).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedAddresses).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
