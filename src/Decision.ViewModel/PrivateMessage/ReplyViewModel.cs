using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Decision.ViewModel.PrivateMessage
{
    public class ReplyViewModel
    {
        [DisplayName("محتوای پیغام")]
        [Required(ErrorMessage = "لطفا محتوای پیغام را مشخص کنید")]
        public string Body { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        [Required]
        public Guid ReplyId { get; set; }
    }
}
