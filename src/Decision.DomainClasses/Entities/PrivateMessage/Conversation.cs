using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.PrivateMessage
{
    /// <summary>
    /// Indicate one conversation
    /// </summary>
    public class Conversation
    {
        #region Ctor
        /// <summary>
        /// create one instance of <see cref="Conversation"/>
        /// </summary>
        public Conversation()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            SentOn = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// gets or sets identifier of record
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// represents this conversaion is seen
        /// </summary>
        public virtual bool IsRead { get; set; }
        /// <summary>
        /// gets or sets subject of this conversation
        /// </summary>
        public virtual string Subject { get; set; }
        /// <summary>
        /// gets or sets Date that this record added
        /// </summary>
        public virtual DateTime SentOn { get; set; }
        /// <summary>
        /// indicate this record deleted by sender
        /// </summary>
        public virtual bool DeletedBySender { get; set; }
        /// <summary>
        /// indicate this record deleted by receiver
        /// </summary>
        public virtual bool DeletedByReceiver { get; set; }
        /// <summary>
        /// gets or sets Messagescount that Unread  by sender of this conversation
        /// </summary>
        public virtual int UnReadSenderMessagesCount { get; set; }
        /// <summary>
        /// gets or sets Messagescount that Unread  by receiver of this conversation
        /// </summary>
        public virtual int UnReadReceiverMessagesCount { get; set; }
        /// <summary>
        /// gets or sets Messagescount of this conversation for increase performance
        /// </summary>
        public virtual int MessagesCount { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// gets or sets if of  user that start this conversation
        /// </summary>
        public virtual long SenderId { get; set; }
        /// <summary>
        /// gets or sets user that start this conversation
        /// </summary>
        public virtual User Sender { get; set; }
        /// <summary>
        /// gets or sets id of  user that is recipient
        /// </summary>
        public virtual long ReceiverId { get; set; }
        /// <summary>
        /// gets or sets   user that is recipient
        /// </summary>
        public virtual User Receiver { get; set; }
        /// <summary>
        /// get or set Messages of this conversation
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }
        #endregion
    }
}
