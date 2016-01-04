using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.ViewModel.Home;

namespace Decision.ServiceLayer.Contracts.Common
{
    /// <summary>
    /// مشخص کننده الزاماتی که ارائه دهنده سرویس باید رعایت کند
    /// </summary>
    public interface IAuditLogService
    {
        void Create(string description, AuditLogType type);
        void Create(string description, string propertyValue);
        IList<LastActivityViewModel> GetLastActivities();
    }
}
