using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Decision.Common.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.Security;
using Decision.Utility;
using Decision.ViewModel.Role;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Users
{
    public class RoleManager : RoleManager<Role, Guid>, IApplicationRoleManager
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionService _permissionService;
        private readonly IDbSet<Role> _roles;
        #endregion

        #region Constructor
        public RoleManager(IMappingEngine mappingEngine, IPermissionService permissionService, IUnitOfWork unitOfWork, IRoleStore<Role, Guid> roleStore)
            : base(roleStore)
        {
            _unitOfWork = unitOfWork;
            _roles = _unitOfWork.Set<Role>();
            _permissionService = permissionService;
            _mappingEngine = mappingEngine;
            AutoCommitEnabled = true;
        }
        #endregion

        #region FindRoleByName
        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }
        #endregion

        #region CreateRole
        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }
        #endregion

        #region GetUsersOfRole
        public IList<UserRole> GetUsersOfRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }

        #endregion

        #region GetApplicationUsersInRole
        public IList<User> GetApplicationUsersInRole(string roleName)
        {
            //var roleUserIdsQuery = from role in Roles
            //                       where role.Name == roleName
            //                       from user in role.Users
            //                       select user.UserId;

            return null; //_userManager.GetUsersWithRoleIds(roleUserIdsQuery.ToArray());
        }
        #endregion

        #region FindUserRoles
        public IList<string> FindUserRoles(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<Guid> FindUserRoleIds(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.Select(a => a.Id).ToList();
        }

        public async Task<IList<Guid>> FindUserRoleIdsAsync(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return await userRolesQuery.Select(a => a.Id).ToListAsync();
        }


        public async Task<IList<string>> FindUserPermissions(Guid userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select new {role.Name, role.Permissions};

            var roles = await userRolesQuery.AsNoTracking().ToListAsync();
            var roleNames = roles.Select(a => a.Name).ToList();
            return
                roleNames.Union(
                    _permissionService.GetUserPermissionsAsList(
                        roles.Select(a => XElement.Parse(a.Permissions)).ToList())).ToList();
        }
        #endregion

        #region GetRolesForUser
        public string[] GetRolesForUser(Guid userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.ToArray();
        }

        #endregion

        #region IsUserInRole
        public bool IsUserInRole(Guid userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        #endregion

        #region GetAllRolesAsync
        public async Task<IList<Role>> GetAllRolesAsync()
        {
            return await Roles.ToListAsync();
        }

        #endregion

        #region SeedDatabase
        /// <summary>
        /// for instal permissions with roles
        /// </summary>
        public void SeedDatabase()
        {
            var standardRoles = StandardRoles.SystemRolesWithPermissions;

            foreach (var role in from record in standardRoles
                                 let role = this.FindByName(record.RoleName)
                                 where role == null
                                 select new Role
                                     {
                                         Name = record.RoleName,
                                         IsSystemRole = true,
                                         XmlPermissions =
                                             _permissionService.GetPermissionsAsXml(record.Permissions.Select(a => a.Name).ToArray())
                                     })
            {
                _roles.Add(role);
            }

            _unitOfWork.SaveChanges();
        }

        #endregion

        #region DeleteAll
        public async Task RemoteAll()
        {
            await Roles.DeleteAsync();
        }
        #endregion

        #region GetList


        public async Task<IEnumerable<RoleViewModel>> GetList()
        {
            return await _roles.Project(_mappingEngine).To<RoleViewModel>().ToListAsync();
        }
        #endregion

        #region AddRole
        public async Task<RoleViewModel> AddRole(AddRoleViewModel viewModel)
        {
            var role = _mappingEngine.Map<Role>(viewModel);
            _roles.Add(role);
            await _unitOfWork.SaveChangesAsync();
            return await GetRole(role.Id);
        }
        #endregion

        #region GetRoleByPermissions

        public async Task<EditRoleViewModel> GetRoleByPermissionsAsync(Guid id)
        {
            var role = await _roles.FirstOrDefaultAsync(r => r.Id == id);
            var viewModel = _mappingEngine.Map<EditRoleViewModel>(role);
            if (role.Permissions != null)
                viewModel.PermissionNames = _permissionService.GetUserPermissionsAsList(role.XmlPermissions).ToArray();
            return viewModel;
        }

        #endregion

        #region EditRole

        public async Task<bool> EditRole(EditRoleViewModel viewModel)
        {
            if (viewModel.PermissionNames == null || viewModel.PermissionNames.Length < 1)
                return false;
            var role = await ((DbSet<Role>)_roles).FindAsync(viewModel.Id);
            _mappingEngine.Map(viewModel, role);
            role.XmlPermissions = _permissionService.GetPermissionsAsXml(viewModel.PermissionNames);
            return true;
        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<Role> roles)
        {
            _unitOfWork.AddThisRange(roles);
        }
        #endregion

        #region AnyAsync
        public Task<bool> AnyAsync()
        {
            return _roles.AnyAsync();
        }
        public bool Any()
        {
            return _roles.Any();
        }
        #endregion

        #region AutoCommitEnabled
        public bool AutoCommitEnabled { get; set; }
        #endregion

        #region CheckForExisByName
        public bool CheckForExisByName(string name, Guid? id)
        {
            var roles = _roles.Select(a => new { Id = a.Id, Name = a.Name }).ToList();
            return id == null ? roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : roles.Any(a => id.Value != a.Id && a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName());
        }
        #endregion

        #region GetPageList
        public async Task<RoleListViewModel> GetPageList(RoleSearchRequest request)
        {
            var roles = _roles.AsNoTracking().AsQueryable();

            if (request.Term.HasValue())
                roles = roles.Where(a => a.Name.Contains(request.Term));

            var selectedTitles = roles.ProjectTo<RoleViewModel>(_mappingEngine);

            var query =await selectedTitles
                .OrderBy(a => a.Name)
                .Skip((request.PageIndex - 1)*5)
                .Take(5)
                .ToListAsync();

            query.ForEach(a =>
            {
                if (a.Permissions != null)
                    a.PermissionsList = _permissionService.GetDescriptions(a.XmlPermission);
            });
            return new RoleListViewModel { SearchRequest = request, Roles = query };
        }
        #endregion

        #region RemoveById
        public async Task RemoveById(Guid id)
        {
            await _roles.Where(a => a.Id == id).DeleteAsync();
        }

        #endregion

        #region CheckRoleIsSystemRoleAsync
        public async Task<bool> CheckRoleIsSystemRoleAsync(Guid id)
        {
            return await _roles.AnyAsync(a => a.Id == id && a.IsSystemRole);
        }
        #endregion

        #region GetAllAsSelectList
        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectList()
        {
            var roles =
                await _roles.AsNoTracking().Project(_mappingEngine).To<SelectListItem>().Cacheable().ToListAsync();
            return roles;
        }
        #endregion

        #region IsInDb
        public Task<bool> IsInDb(Guid id)
        {
            return _roles.AnyAsync(a => a.Id == id);
        }
        #endregion

        #region Fill


        public void FillForEdit(EditRoleViewModel viewModel)
        {

            var permissions = AssignableToRolePermissions.GetAsSelectListItems();

            var selectListItems = permissions as IList<SelectListItem> ?? permissions.ToList();
            if (viewModel.PermissionNames != null)
            {
                selectListItems.ToList().ForEach(
                    a => a.Selected = viewModel.PermissionNames.Any(s => s == a.Value));
            }

            viewModel.Permissions = selectListItems;
        }
        #endregion

        #region GetRole
        public async Task<RoleViewModel> GetRole(Guid id)
        {
            var role = await _roles.FirstAsync(r => r.Id == id);
            var viewModel = _mappingEngine.Map<RoleViewModel>(role);
            if (viewModel.Permissions != null)
                viewModel.PermissionsList = _permissionService.GetDescriptions(viewModel.XmlPermission);
            return viewModel;
        }
        #endregion

    }
}
