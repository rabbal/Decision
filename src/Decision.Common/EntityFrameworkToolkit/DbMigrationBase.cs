using System.Data.Entity.Migrations;
using System.Reflection;

namespace Decision.Common.EntityFrameworkToolkit
{
    public abstract class DbMigrationBase : DbMigration
    {
        protected void ExecSqlResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            SqlResource($"{assemblyName}.Sql.{resourceName}.sql", assembly, true);
        }

    }
}