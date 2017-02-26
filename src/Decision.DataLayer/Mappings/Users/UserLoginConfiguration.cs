using System.Data.Entity.ModelConfiguration;

namespace Decision.DataLayer.Mappings.Users
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
