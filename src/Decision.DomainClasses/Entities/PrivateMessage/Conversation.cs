using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.PrivateMessage
{
    /// <summary>
    /// نشان دهنده گفتگو
    /// </summary>
    public class Conversation
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Conversation()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            Messages = new List<Message>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// آیا گفتگوی جدید مشاهده شده است؟
        /// </summary>
        public bool IsSeen { get; set; }
        /// <summary>
        /// موضوع گفتگو
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// تاریخ آغاز گفتگو
        /// </summary>
        public DateTime StartDate { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی کاربر آغاز کننده گفتگو
        /// </summary>
        public Guid SenderId { get; set; }
        /// <summary>
        ///  کاربر آغاز کننده گفتگو
        /// </summary>
        public User Sender { get; set; }
        /// <summary>
        /// آی دی کاربر پذیرنده گفتگو
        /// </summary>
        public Guid ReceiverId { get; set; }
        /// <summary>
        ///  کاربر پذیرنده گفتگو
        /// </summary>
        public User Receiver { get; set; }
        /// <summary>
        /// پیغام های گفتگو
        /// </summary>
        public ICollection<Message> Messages { get; set; }
        #endregion
    }
}
