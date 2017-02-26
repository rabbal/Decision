using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Identity;

namespace Decision.DataLayer.Mappings.Users
{
    public class UserClaimMap : EntityMap<UserClaim, int>
    {
        public UserClaimMap()
        {
            ToTable("UserClaims");
        }
    }
}
