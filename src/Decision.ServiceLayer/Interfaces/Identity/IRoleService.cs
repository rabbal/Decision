using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Decision.DomainClasses.Identity;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.Interfaces.Identity
{
    public interface IRoleService : IDisposable
    {

        string[] GetRolesForUser(long userId);
        bool IsUserInRole(long userId, string roleName);
        IEnumerable<long> FindUserRoleIds(long userId);
        Task<IList<long>> FindUserRoleIdsAsync(long userId);
        IList<string> FindUserRoles(long userId);
        Role FindRoleByName(string roleName);
        IdentityResult CreateRole(Role role);
        IList<UserRole> GetUsersOfRole(string roleName);
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> UpdateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<bool> RoleExistsAsync(string roleName);
        Task<Role> FindByIdAsync(long roleId);
        Task<Role> FindByNameAsync(string roleName);
        IIdentityValidator<Role> RoleValidator { get; set; }
        IQueryable<Role> Roles { get; }
        void CreateSystemRole(string roleName,string displayName);

    }
}