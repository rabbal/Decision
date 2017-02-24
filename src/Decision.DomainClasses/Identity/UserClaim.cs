using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Decision.DomainClasses.Identity
{
    public class UserClaim : IdentityUserClaim<long>, IEntity<int>
    {
        #region Properties

        public byte[] RowVersion { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }

        #endregion

        #region Public Methods

        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        public void InitializeFromClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}