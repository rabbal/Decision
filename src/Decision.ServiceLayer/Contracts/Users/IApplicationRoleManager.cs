using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.Users;
using Decision.ViewModel.Role;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.Contracts.Users
{
    public interface IApplicationRoleManager : IDisposable
    {
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<Role> RoleValidator { get; set; }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(Role role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(Role role);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(Role role);

        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<Role> FindByIdAsync(Guid roleId);

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<Role> FindByNameAsync(string roleName);
        // Our new custom methods

        Role FindRoleByName(string roleName);
        IdentityResult CreateRole(Role role);
        IList<UserRole> GetUsersOfRole(string roleName);
        IList<User> GetApplicationUsersInRole(string roleName);
        IList<string> FindUserRoles(Guid userId);
        string[] GetRolesForUser(Guid userId);
        bool IsUserInRole(Guid userId, string roleName);
        Task<IList<Role>> GetAllRolesAsync();
        void SeedDatabase();
        Task RemoteAll();
        Task<IEnumerable<RoleViewModel>> GetList();
        Task<RoleViewModel> AddRole(AddRoleViewModel viewModel);
        Task<bool> EditRole(EditRoleViewModel viewModel);
        Task<EditRoleViewModel> GetRoleByPermissionsAsync(Guid id);
        void AddRange(IEnumerable<Role> roles);
        Task<bool> AnyAsync();
        bool AutoCommitEnabled { get; set; }
        bool Any();
        bool CheckForExisByName(string name, Guid? id);
        Task RemoveById(Guid id);
        Task<bool> CheckRoleIsSystemRoleAsync(Guid id);

        /// <summary>
        /// واکشی لیست گروه ها کاربری
        /// </summary>

        /// <returns></returns>
        Task<RoleListViewModel> GetPageList(RoleSearchRequest request);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectList();
        IEnumerable<Guid> FindUserRoleIds(Guid userId);
        Task<IList<string>> FindUserPermissions(Guid userId);
        Task<IList<Guid>> FindUserRoleIdsAsync(Guid userId);
        /// <summary>
        /// چک کردن برای موچود بود در دیتابیس
        /// </summary>
        /// <param name="id">آی دی</param>
        /// <returns></returns>
        Task<bool> IsInDb(Guid id);
        /// <summary>
        /// پر کردن لیست های مربوطه
        /// </summary>
        /// <returns></returns>
        void FillForEdit(EditRoleViewModel viewModel);

        Task<RoleViewModel> GetRole(Guid id);

    }
}
