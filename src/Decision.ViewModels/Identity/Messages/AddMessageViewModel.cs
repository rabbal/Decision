using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModels.Identity.Messages
{
    public class AddMessageViewModel
    {
        [DisplayName("محتوای پیغام")]
        [Required(ErrorMessage = "لطفا محتوای پیغام را مشخص کنید")]
        [AllowHtml]
        public string Body { get; set; }
        [Required]
        public Guid ConversationId { get; set; }
        [Required]
        public Guid ParentId { get; set; }
    }
}
