using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Configurations.TeacherInfo
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

            HasRequired(a=>a.Teacher).WithMany(j=>j.Addresses).HasForeignKey(a=>a.TeacherId).WillCascadeOnDelete(true);
            HasRequired(e => e.Creator).WithMany(u => u.CreatedAddresses).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(false);
            HasOptional(e => e.LasModifier).WithMany(u => u.ModifiedAddresses).HasForeignKey(e => e.LasModifierId).WillCascadeOnDelete(false);
        }
    }
}
