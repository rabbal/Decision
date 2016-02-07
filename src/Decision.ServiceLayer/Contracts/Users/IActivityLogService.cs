using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.ViewModel.Home;

namespace Decision.ServiceLayer.Contracts.Users
{
    /// <summary>
    /// مشخص کننده الزاماتی که ارائه دهنده سرویس باید رعایت کند
    /// </summary>
    public interface IActivityLogService
    {
        void Create(ActivityLog log);
        IList<LastActivityViewModel> GetLastActivities();
    }
}
