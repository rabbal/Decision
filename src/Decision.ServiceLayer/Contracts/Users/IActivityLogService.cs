using System.Collections.Generic;
using Decision.DomainClasses.Users;
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
