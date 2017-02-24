using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Users;

namespace Decision.DataLayer.Configurations.Users
{
   public class UserLoginConfiguration:EntityTypeConfiguration<UserLogin>
   {
       public UserLoginConfiguration()
       {
           HasKey(l => new {l.LoginProvider, l.ProviderKey, l.UserId});
               ToTable("UserLogins");
       }
    }
}
