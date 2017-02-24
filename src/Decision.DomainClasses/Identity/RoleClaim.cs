using System.Security.Claims;
using Decision.Common.Domain;

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
        public Role RoleId { get; set; }

        #endregion
    }
}