using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using Decision.DataLayer.Context;


namespace Decision.DataLayer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
       
        public Configuration()
        {
            
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
           
        }

    }
}
