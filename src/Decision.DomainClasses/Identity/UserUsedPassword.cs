using System;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Identity
{
    public class UserUsedPassword : Entity
    {
        #region Properties

        public string HashedPassword { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public long UserId { get; set; }

        #endregion
    }
}