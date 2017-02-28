using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Uow;
using Decision.ServiceLayer.Interfaces.Identity;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class RoleService : RoleManager<Role, long>, IRoleService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Role> _roles;
        #endregion

        #region Constructor
        public RoleService(IRoleStore<Role, long> roleStore, IUnitOfWork unitOfWork)
            : base(roleStore)
        {
            _unitOfWork = unitOfWork;
            _roles = _unitOfWork.Set<Role>();
        }

        #endregion

        #region Public Methods
        public string[] GetRolesForUser(long userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.ToArray();
        }
        public bool IsUserInRole(long userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }
        public IEnumerable<long> FindUserRoleIds(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.Select(a => a.Id).ToList();
        }
        public async Task<IList<long>> FindUserRoleIdsAsync(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return await  userRolesQuery.Select(a => a.Id).ToListAsync().ConfigureAwait(false);
        }
        public IList<string> FindUserRoles(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToList();
        }
        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }
        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }
        public IList<UserRole> GetUsersOfRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }

        public void CreateSystemRole(string roleName, string displayName)
        {
            if (CheckNameExist(roleName)) return;
            _roles.Add(new Role { Name = roleName, IsSystemEntry = true, DisplayName = displayName });
            _unitOfWork.SaveChanges();
        }

        private bool CheckNameExist(string roleName)
        {
            return _roles.Any(a => a.Name == roleName);
        }
        #endregion
    }
}
