using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Decision.ViewModel.PrivateMessage
{
    public class AddConversationViewModel
    {
        [DisplayName("موضوع پیغام")]
        [Required(ErrorMessage = "لطفا موضوع پیغام را مشخص کنید")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "لطفا محتوای پیغام را مشخص کنید")]
        [DisplayName("محتوای پیغام")]
        [AllowHtml]
        public string Body { get; set; }
        [Required(ErrorMessage = "لطفا دریافت کننده پیغام را مشخص کنید")]
        [DisplayName("دریافت کننده")]
        public Guid ReciverId { get; set; }
        public IEnumerable<SelectListItem> Users  { get; set; }
    }
}
