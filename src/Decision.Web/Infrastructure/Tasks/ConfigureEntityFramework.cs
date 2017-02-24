using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using ElmahEFLogger.CustomElmahLogger;
using Decision.DataLayer.Context;
using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    public class ConfigureEntityFramework : IBootstrapperTask
    {
        private readonly IUnitOfWork _unitOfWork;
        public int Order => int.MaxValue;

        public ConfigureEntityFramework(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            _unitOfWork.ForceDatabaseInitialize();
            DbInterception.Add(new ElmahEfInterceptor());
        }
    }
}