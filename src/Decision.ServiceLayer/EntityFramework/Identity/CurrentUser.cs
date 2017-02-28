using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Uow;
using Decision.ServiceLayer.Interfaces.Identity;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class CurrentUser : ICurrentUser
    {
        #region Constructor

        public CurrentUser(IUnitOfWork unitOfWork, IIdentity identity, IPrincipal principal)
        {
            _identity = identity;
            _principal = principal;
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
        }

        #endregion

        #region Fields 

        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentity _identity;
        private readonly IPrincipal _principal;
        private User _user;
        private readonly IDbSet<User> _users;

        #endregion

        #region Properties 
        public string DisplayName => ((ClaimsPrincipal) _principal).FindFirst(ClaimTypes.Surname).
            Value;


        public bool IsInRole(string roleName)
        {
            return _principal.IsInRole(roleName);
        }
        
        public User User => _user ?? (_user = _users.Find(Id));
        public long Id => _identity.GetUserId<long>();

        public string UserName => _identity.GetUserName();

        public bool IsAuthenticated => _identity != null && _identity.IsAuthenticated;

        #endregion
    }
}