using System;
using System.Collections.Generic;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Messages
{
    public class Message : Entity<Guid>
    {
        #region Ctor

        public Message()
        {

            SentOn = DateTime.Now;
        }

        #endregion

        #region Properties
    
        public bool IsRead { get; set; }

        public string Body { get; set; }

        public DateTime SentOn { get; set; }

        #endregion

        #region NavigationProperties

        public Guid? ParentId { get; set; }

        public Message Parent { get; set; }

        public ICollection<Message> Children { get; set; }

        public long SenderUserId { get; set; }

        public User SenderUser { get; set; }

        public Conversation Conversation { get; set; }

        public Guid ConversationId { get; set; }

        #endregion
    }
}