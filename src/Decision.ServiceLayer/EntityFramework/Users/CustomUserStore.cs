using System;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Interfaces.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.ServiceLayer.EntityFramework.Users
{
    public class CustomUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        public CustomUserStore(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
