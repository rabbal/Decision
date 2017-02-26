using System;
using Decision.DomainClasses.Messages;

namespace Decision.DataLayer.Mappings.Messages
{
    public class MessageMap : EntityMap<Message, Guid>
    {
        public MessageMap()
        {
            Property(m => m.Body).IsMaxLength().IsRequired();

            HasMany(m => m.Children).WithOptional(m => m.Parent).HasForeignKey(m => m.ParentId).WillCascadeOnDelete(false);
            HasRequired(m => m.SenderUser).WithMany().HasForeignKey(m => m.SenderUserId).WillCascadeOnDelete(true);

        }
    }
}
