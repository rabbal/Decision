using Decision.ServiceLayer.Interfaces.Identity;
using Decision.Services.Interfaces.Users;
using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    public class AddUserAndRoles : IRunStartUp
    {

        public int Order => int.MaxValue;

        #region Fields (1)

        private readonly IUserService _userService;

        #endregion

        #region Ctor (1)

        public AddUserAndRoles(IUserService userService)
        {
            _userService = userService;
        }

        #endregion
        
        #region Methods (1)

        public void Execute()
        {

        }

        #endregion
    }
}