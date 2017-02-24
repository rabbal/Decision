using System.Data.Entity;
using Decision.Common.Domain;
using Decision.Common.GuardToolkit;
using Decision.DataLayer.Context;

namespace Decision.ServiceLayer.EntityFramework
{
    public abstract class ServiceBase<TEntity> where TEntity : class, IEntity
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TEntity> _set;

        #endregion

        #region Constructor

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            Check.ArgumentNotNull(unitOfWork, nameof(unitOfWork));

            _unitOfWork = unitOfWork;
            _set = _unitOfWork.Set<TEntity>();
        }
        #endregion

        #region Public Methods

        #endregion
    }
}
