using System.Security.Claims;
using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Identity
{
    public class RoleClaim : Entity
    {
        #region Properties

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

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

        #endregion

        #region Navigation Properties

        public Role Role { get; set; }
        public long RoleId { get; set; }

        #endregion
    }
}