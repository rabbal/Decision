using System;
using Decision.DomainClasses.Messages;

namespace Decision.DataLayer.Mappings.Messages
{
    public class ConversationMap:EntityMap<Conversation,Guid>
    {
        public ConversationMap()
        {
            Property(c => c.Subject).HasMaxLength(1024).IsRequired();

            HasMany(c => c.Messages)
                .WithRequired(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .WillCascadeOnDelete(true);

            HasRequired(c => c.SenderUser)
                .WithMany()
                .HasForeignKey(c => c.SenderUserId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.ReceiverUser)
                .WithMany()
                .HasForeignKey(c => c.ReceiverUserId)
                .WillCascadeOnDelete(false);

        }
    }
}
