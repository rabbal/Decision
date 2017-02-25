using System;
using System.Collections.Generic;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses.Messages
{
    public class Conversation
    {
        #region Ctor

        public Conversation()
        {
            SentOn = DateTime.Now;
        }

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; }

        public DateTime SentOn { get; set; }

        public bool DeletedBySender { get; set; }

        public bool DeletedByReceiver { get; set; }

        public int UnReadSenderMessagesCount { get; set; }

        public int UnReadReceiverMessagesCount { get; set; }
        public int MessagesCount { get; set; }

        #endregion

        #region NavigationProperties

        public Guid SenderId { get; set; }

        public User Sender { get; set; }

        public Guid ReceiverId { get; set; }

        public User Receiver { get; set; }

        public ICollection<Message> Messages { get; set; }

        #endregion
    }
}