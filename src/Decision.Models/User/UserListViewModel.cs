using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ViewModel.User
{
    public class UserListViewModel
    {
        /// <summary>
        /// لیست کاربران
        /// </summary>
        public IList<UserViewModel> Users { get; set; }
        /// <summary>
        /// درخواست
        /// </summary>
        public UserSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// لیست گروه های کاربری برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
