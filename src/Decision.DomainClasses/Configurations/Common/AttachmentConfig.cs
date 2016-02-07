using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Configurations.Common
{
    /// <summary>
    /// مشخص کننده کلاس مپینگ مربوط به فایل های ضمیمه
    /// </summary>
    public class AttachmentConfig : EntityTypeConfiguration<Attachment>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AttachmentConfig()
        {
            
        }
    }
}
