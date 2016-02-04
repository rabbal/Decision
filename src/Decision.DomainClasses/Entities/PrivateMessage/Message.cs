using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.PrivateMessage
{
   
        /// <summary>
        /// Represents One Reply to Conversation
        /// </summary>
        public class Message
        {
            #region Ctor
            /// <summary>
            /// create one instance of <see cref="Message"/>
            /// </summary>
            public Message()
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
            /// represents this conversaionReply is seen
            /// </summary>
            public virtual bool IsRead { get; set; }
            /// <summary>
            /// gets or sets body of this conversationReply
            /// </summary>
            public virtual string Body { get; set; }
            /// <summary>
            /// gets or sets Date that this record added
            /// </summary>
            public virtual DateTime SentOn { get; set; }
            #endregion

            #region NavigationProperties
            /// <summary>
            /// gets or sets  Parent's Id Of this ConversationReply
            /// </summary>
            public virtual Guid? ParentId { get; set; }
            /// <summary>
            /// gets or sets Parent Of this ConversationReply
            /// </summary>
            public virtual Message Parent { get; set; }
            /// <summary>
            /// get or set Children Of this ConversationReply
            /// </summary>
            public virtual ICollection<Message> Children { get; set; }
            /// <summary>
            /// gets or sets if of  user that start this conversationReply
            /// </summary>
            public virtual long SenderId { get; set; }
            /// <summary>
            /// gets or sets user that start this conversationReply
            /// </summary>
            public virtual User Sender { get; set; }
            /// <summary>
            /// gets or sets Conversation that this message sent in it 
            /// </summary>
            public virtual Conversation Conversation { get; set; }
            /// <summary>
            /// gets or sets Id of Conversation that this message sent in it 
            /// </summary>
            public virtual Guid ConversationId { get; set; }
            #endregion
        
    }
}
