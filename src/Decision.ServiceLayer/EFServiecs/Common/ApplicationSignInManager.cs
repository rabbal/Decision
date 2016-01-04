using System;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ServiceLayer.EFServiecs.Users;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    public class ApplicationSignInManager : SignInManager<User, Guid>, IApplicationSignInManager
    {

        #region Fields
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        #endregion

        #region Constructor

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
        #endregion

       
    }
}
