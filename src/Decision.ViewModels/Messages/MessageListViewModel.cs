using System.Collections.Generic;
using Decision.DomainClasses.Messages;

namespace Decision.ViewModels.Messages
{
    public class MessageListViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public AddMessageViewModel AddMessageViewModel { get; set; }
    }
}
