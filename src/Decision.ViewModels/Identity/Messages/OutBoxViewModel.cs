using System;

namespace Decision.ViewModels.Identity.Messages
{
    public class OutBoxViewModel
    {
        public Guid Id { get; set; }
        public bool IsSeen { get; set; }
        public string Subject { get; set; }
        public DateTime SentOn { get; set; }
        public string DisPlayName { get; set; }

    }
}
