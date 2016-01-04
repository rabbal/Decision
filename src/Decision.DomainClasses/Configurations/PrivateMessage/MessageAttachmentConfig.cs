using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.PrivateMessage;

namespace Decision.DomainClasses.Configurations.PrivateMessage
{
    /// <summary>
    /// نشان دهنده مپینگ مربوط به کلاس ضمیمه های پیغام
    /// </summary>
    public class MessageAttachmentConfig:EntityTypeConfiguration<MessageAttachment>
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageAttachmentConfig()
        {
            Property(ma => ma.FriendlyName).HasMaxLength(256).IsRequired();
        }
    }
}
