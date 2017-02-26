using System;
using Decision.Framework.Domain;
using Decision.Framework.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class UserLogin : IdentityUserLogin<long>
    {
        #region Navigation Properties

        public User User { get; set; }

        #endregion
    }
}