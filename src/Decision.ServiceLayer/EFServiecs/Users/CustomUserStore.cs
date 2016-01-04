using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Users;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.ServiceLayer.EFServiecs.Users
{
    public class CustomUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        public CustomUserStore(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
