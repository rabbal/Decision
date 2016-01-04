using System.Data;
using System.Data.Entity;
using System.Web;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Contracts.Common;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    public class TransactionPerRequest : IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpContextBase _httpContext;
        private const string Transaction = "_Transaction";
        private const string Error = "_Error";
        #endregion

        #region Ctor
        public TransactionPerRequest(IUnitOfWork unitOfWork, HttpContextBase httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;

        }

        #endregion

        #region Interfaces
        void IRunOnEachRequest.Execute()
        {
            _httpContext.Items[Transaction] =
            _unitOfWork.Database.BeginTransaction(IsolationLevel.Snapshot);
        }

        void IRunOnError.Execute()
        {
            _httpContext.Items[Error] = true;
        }

        void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction)_httpContext.Items["_Transaction"];
            if (_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }
        #endregion

    }
}
