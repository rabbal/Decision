using System.Data;
using System.Data.Entity;
using System.Web;
using Decision.DataLayer.Context;
using Decision.Web.Infrastructure.Tasks.Contracts;

namespace Decision.Web.Infrastructure.Tasks
{
    /// <summary>
    ///     The transaction per request.
    /// </summary>
    public class TransactionPerRequest : IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        #region Ctor (1)
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransactionPerRequest" /> class.
        /// </summary>
        /// <param name="unitOfWork">the unit of work </param>
        /// <param name="httpContext">the base http context</param>
        public TransactionPerRequest(IUnitOfWork unitOfWork, HttpContextBase httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        #endregion

        #region Constants (2)

        private const string Error = "_Error";
        private const string Transaction = "_Transaction";

        #endregion

        #region Fields (2)

        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpContextBase _httpContext;

        int IRunOnEachRequest.Order => 1;
        int IRunAfterEachRequest.Order => 1;
        int IRunOnError.Order => 1;

        #endregion

        #region Interfaces (3)

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
            var transaction = (DbContextTransaction) _httpContext.Items[Transaction];
            if (_httpContext.Items[Error] != null)
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