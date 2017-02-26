using System;
using System.Collections.Generic;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Messages
{
    public class Conversation : Entity<Guid>
    {
        #region Constructor

        public Conversation()
        {
            SumbitDateTime = DateTime.Now;
            Messages = new HashSet<Message>();
        }

        #endregion

        #region Properties

        public bool IsRead { get; set; }
        public string Subject { get; set; }
        public DateTime SumbitDateTime { get; set; }
        public bool DeletedBySender { get; set; }
        public bool DeletedByReceiver { get; set; }

        #endregion

        #region NavigationProperties

        public long SenderUserId { get; set; }

        public User SenderUser { get; set; }

        public Guid ReceiverUserId { get; set; }

        public User ReceiverUser { get; set; }

        public ICollection<Message> Messages { get; set; }

        #endregion
    }
}