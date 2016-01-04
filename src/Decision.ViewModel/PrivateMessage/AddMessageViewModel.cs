using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModel.PrivateMessage
{
    public class AddMessageViewModel
    {
        [DisplayName("محتوای پیغام")]
        [Required(ErrorMessage = "لطفا محتوای پیغام را مشخص کنید")]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        [Required]
        public Guid ReplyId { get; set; }
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }
    }
}
