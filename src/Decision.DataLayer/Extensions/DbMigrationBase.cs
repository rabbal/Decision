using System.Data.Entity.Migrations;
using System.Reflection;

namespace Decision.DataLayer.Extensions
{
    public abstract class DbMigrationBase : DbMigration
    {
        public void ExecSqlResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            SqlResource($"{assemblyName}.Sql.{resourceName}.sql", assembly, true);
        }

    }
}