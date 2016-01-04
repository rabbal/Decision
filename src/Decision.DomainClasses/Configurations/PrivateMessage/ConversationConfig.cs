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
    /// نشان دهنده مپینگ مربوط به کلاس گفتگو
    /// </summary>
    public class ConversationConfig:EntityTypeConfiguration<Conversation>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConversationConfig()
        {
            Property(c => c.Subject).HasMaxLength(1024).IsRequired();

            HasMany(c => c.Messages)
                .WithRequired(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .WillCascadeOnDelete(true);

            HasRequired(c => c.Sender)
                .WithMany(u => u.SentConversations)
                .HasForeignKey(c => c.SenderId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Receiver)
                .WithMany(u => u.ReceivedConversations)
                .HasForeignKey(c => c.ReceiverId)
                .WillCascadeOnDelete(false);

        }
    }
}
