using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ViewModels.Identity
{
    public class UserListViewModel
    {
        public IList<UserViewModel> Users { get; set; }
        public UserSearchRequest SearchRequest { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
