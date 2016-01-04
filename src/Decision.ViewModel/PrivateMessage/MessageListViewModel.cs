using System.Collections.Generic;

namespace Decision.ViewModel.PrivateMessage
{
    public class MessageListViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public AddMessageViewModel AddMessageViewModel { get; set; }
    }
}
