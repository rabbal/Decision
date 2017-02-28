using System.Data.Entity;
using Decision.DomainClasses.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class UserStore :
        UserStore<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        public UserStore(DbContext context)
            : base(context)
        {
        }
    }
}