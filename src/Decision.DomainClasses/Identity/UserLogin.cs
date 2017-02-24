using System;
using Decision.Common.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class UserLogin : IdentityUserLogin<long>, IEntity<long>
    {
        #region Navigation Properties

        public User User { get; set; }

        #endregion

        #region Public Methods

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        public long Id { get; set; }
        public byte[] RowVersion { get; set; }

        #endregion
    }
}