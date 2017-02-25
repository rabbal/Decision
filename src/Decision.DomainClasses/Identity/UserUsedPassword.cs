using System;
using Decision.Framework.Domain;

namespace Decision.DomainClasses.Identity
{
    public class UserUsedPassword : Entity<Guid>
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