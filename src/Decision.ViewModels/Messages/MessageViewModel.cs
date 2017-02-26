using System;
using System.Collections.Generic;

namespace Decision.ViewModels.Messages
{
    public class MessageViewModel
    {
        #region Properties
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        #endregion

        #region NavigationProperties
        public virtual Guid? ReplyId { get; set; }
       
        public string SenderUserName { get; set; }
        public  ICollection<AttachmentViewModel> Attachments { get; set; }
      
        #endregion
    }
}
