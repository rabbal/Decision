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
    /// نشان دهنده مپینگ مربوط به کلاس پیغام
    /// </summary>
    public class MessageConfig : EntityTypeConfiguration<Message>
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageConfig()
        {
            Property(m => m.Content).IsMaxLength().IsRequired();

            HasMany(m => m.Attachments)
                .WithRequired(a => a.Message)
                .HasForeignKey(a => a.MessageId)
                .WillCascadeOnDelete(true);

            HasMany(m => m.Children).WithOptional(m => m.Reply).HasForeignKey(m => m.ReplyId).WillCascadeOnDelete(false);
            HasRequired(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(m => m.SenderId).WillCascadeOnDelete(true);

        }
    }
}
