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
        public  Guid Id { get; set; }
        /// <summary>
        /// represents this conversaion is seen
        /// </summary>
        public  bool IsRead { get; set; }
        /// <summary>
        /// gets or sets subject of this conversation
        /// </summary>
        public  string Subject { get; set; }
        /// <summary>
        /// gets or sets Date that this record added
        /// </summary>
        public  DateTime SentOn { get; set; }
        /// <summary>
        /// indicate this record deleted by sender
        /// </summary>
        public  bool DeletedBySender { get; set; }
        /// <summary>
        /// indicate this record deleted by receiver
        /// </summary>
        public  bool DeletedByReceiver { get; set; }
        /// <summary>
        /// gets or sets Messagescount that Unread  by sender of this conversation
        /// </summary>
        public  int UnReadSenderMessagesCount { get; set; }
        /// <summary>
        /// gets or sets Messagescount that Unread  by receiver of this conversation
        /// </summary>
        public  int UnReadReceiverMessagesCount { get; set; }
        /// <summary>
        /// gets or sets Messagescount of this conversation for increase performance
        /// </summary>
        public  int MessagesCount { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// gets or sets if of  user that start this conversation
        /// </summary>
        public  Guid SenderId { get; set; }
        /// <summary>
        /// gets or sets user that start this conversation
        /// </summary>
        public  User Sender { get; set; }
        /// <summary>
        /// gets or sets id of  user that is recipient
        /// </summary>
        public  Guid ReceiverId { get; set; }
        /// <summary>
        /// gets or sets   user that is recipient
        /// </summary>
        public  User Receiver { get; set; }
        /// <summary>
        /// get or set Messages of this conversation
        /// </summary>
        public  ICollection<Message> Messages { get; set; }
        #endregion
    }
}
