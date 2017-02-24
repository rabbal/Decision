namespace Decision.DataLayer.ExecutionStrategy
{
    using System.Data.Entity;

    public class CustomDbConfiguration : DbConfiguration
    {
        public CustomDbConfiguration()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlServerExecutionStrategy());
            //SetManifestTokenResolver(new CustomManifestTokenResolver());
        }
    }
}