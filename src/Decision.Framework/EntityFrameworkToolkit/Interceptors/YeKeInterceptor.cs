using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using Decision.Framework.Utility;

namespace Decision.Framework.EntityFrameworkToolkit.Interceptors
{
    public class YeKeInterceptor : IDbCommandInterceptor
    {
        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            command.ApplyCorrectYeKe();
        }
    }
}
