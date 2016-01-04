using System;
using System.Collections.Generic;

namespace Decision.ViewModel.PrivateMessage
{
    public class MessageViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// محتوای پیغام
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// تاریخ ارسال
        /// </summary>
        public DateTime SendDate { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی  پیغامی که این پیغام به عنوان پاسخ آن است
        /// </summary>
        public virtual Guid? ReplyId { get; set; }
       
        public string SenderUserName { get; set; }
        /// <summary>
        /// ضمیمه های پیغام
        /// </summary>
        public  ICollection<AttachmentViewModel> Attachments { get; set; }
      
        #endregion
    }
}
