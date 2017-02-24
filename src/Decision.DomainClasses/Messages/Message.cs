using System;
using System.Collections.Generic;

namespace Decision.DomainClasses.PrivateMessage
{
    public class Message
    {
        #region Ctor

        public Message()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            SentOn = DateTime.Now;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public bool IsRead { get; set; }

        public string Body { get; set; }

        public DateTime SentOn { get; set; }

        #endregion

        #region NavigationProperties

        public Guid? ParentId { get; set; }

        public Message Parent { get; set; }

        public ICollection<Message> Children { get; set; }

        public Guid SenderId { get; set; }

        public User Sender { get; set; }

        public Conversation Conversation { get; set; }

        public Guid ConversationId { get; set; }

        #endregion
    }
}