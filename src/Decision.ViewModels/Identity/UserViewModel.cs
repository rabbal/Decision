using System;

namespace Decision.ViewModels.Identity
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsBanned { get; set; }
        public bool IsSystemAccount { get; set; }
        public string DisplayName { get; set; }
    }
}
