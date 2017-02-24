using System;
using Decision.ServiceLayer.Interfaces.Identity;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EntityFramework.Users
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<Role, Guid> _roleStore;

        public CustomRoleStore(IRoleStore<Role, Guid> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}
