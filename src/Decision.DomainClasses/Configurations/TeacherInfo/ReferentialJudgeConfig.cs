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
    /// نشان دهنده مپینگ مربوط به کلاس ارجاعات اساتید برای ویرایش
    /// </summary>
    public class ReferentialTeacherConfig : EntityTypeConfiguration<ReferentialTeacher>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ReferentialTeacherConfig()
        {
            HasRequired(r => r.ReferencedFrom)
                .WithMany(u => u.SentReferentialTeachers)
                .HasForeignKey(r => r.ReferencedFromId)
                .WillCascadeOnDelete(false);

            HasRequired(r => r.ReferencedTo)
                .WithMany(u => u.ReceivedReferentialTeachers)
                .HasForeignKey(r => r.ReferencedToId)
                .WillCascadeOnDelete(false);


            HasRequired(r => r.Teacher)
                .WithMany(u => u.ReferentialTeachers)
                .HasForeignKey(r => r.TeacherId)
                .WillCascadeOnDelete(true);

            
        }
    }
}
