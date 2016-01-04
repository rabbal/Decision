using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.PrivateMessage
{
    /// <summary>
    /// نشان دهنده پیغام 
    /// </summary>
    public class Message
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public Message()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            SendDate = DateTime.Now;
            Attachments = new List<MessageAttachment>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// محتوای پیغام
        /// </summary>
        public  string Content { get; set; }
        /// <summary>
        /// تاریخ ارسال
        /// </summary>
        public  DateTime SendDate { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی  پیغامی که این پیغام به عنوان پاسخ آن است
        /// </summary>
        public  Guid? ReplyId { get; set; }
        /// <summary>
        ///   پیغامی که این پیغام به عنوان پاسخ آن است
        /// </summary>
        public  Message Reply { get; set; }
        /// <summary>
        /// پاسخ های پیغام
        /// </summary>
        public  ICollection<Message> Children { get; set; }
        /// <summary>
        /// ارسال کننده پیغام
        /// </summary>
        public  User Sender { get; set; }
        /// <summary>
        /// آی دی ارسال کننده پیغام
        /// </summary>
        public  Guid SenderId { get; set; }
        /// <summary>
        /// ضمیمه های پیغام
        /// </summary>
        public  ICollection<MessageAttachment> Attachments { get; set; }
        /// <summary>
        /// گفتگویی که این پیغام مربوط به آن است
        /// </summary>
        public  Conversation Conversation { get; set; }
        /// <summary>
        ///آی د ی گفتگویی که این پیغام مربوط به آن است
        /// </summary>
        public  Guid ConversationId { get; set; }
        #endregion
    }
}
