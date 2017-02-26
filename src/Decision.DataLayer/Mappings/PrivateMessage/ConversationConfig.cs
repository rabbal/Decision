using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.PrivateMessage
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
