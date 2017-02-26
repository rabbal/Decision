using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Decision.ViewModels.Messages
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
