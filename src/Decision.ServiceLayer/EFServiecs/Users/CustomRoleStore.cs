using System;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Users
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
