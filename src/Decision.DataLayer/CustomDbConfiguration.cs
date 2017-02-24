using System.Data.Entity;

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