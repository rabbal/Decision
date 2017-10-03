using System.Data.Entity;
using Decision.Framework.EntityFrameworkToolkit;
using Decision.Framework.EntityFrameworkToolkit.Interceptors;
using ElmahEFLogger.CustomElmahLogger;

namespace Decision.DataLayer
{
    public class CustomDbConfiguration : DbConfiguration
    {
        public CustomDbConfiguration()
        {

            // problem with user defined transaction like [db.Database.BeginTransaction()]
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlServerExecutionStrategy());
            SetManifestTokenResolver(new CustomManifestTokenResolver());
            AddInterceptor(new YeKeInterceptor());
            AddInterceptor(new ElmahEfInterceptor());
        }
    }
}