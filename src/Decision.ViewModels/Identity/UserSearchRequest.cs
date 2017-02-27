using System;
using System.ComponentModel;

namespace Decision.ViewModels.Identity
{
    public class UserSearchRequest : ListRequestBase
    {
        public string UserName { get; set; }
        [DisplayName("گروه کاربری")]
        public Guid? RoleId { get; set; }

    }

    public static class UserSortBy
    {
        public const string UserName = "UserName";
        public const string LastLogingDate = "LastLogingDate";
    }
}
