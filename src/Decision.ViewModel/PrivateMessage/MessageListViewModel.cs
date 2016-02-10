using System.Collections.Generic;
using Decision.DomainClasses.Entities.PrivateMessage;

namespace Decision.ViewModel.PrivateMessage
{
    public class MessageListViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public AddMessageViewModel AddMessageViewModel { get; set; }
    }
}
