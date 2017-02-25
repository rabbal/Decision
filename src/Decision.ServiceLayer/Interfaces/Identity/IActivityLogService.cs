using System.Collections.Generic;
using Decision.ViewModel.Home;

namespace Decision.ServiceLayer.Interfaces.Identity
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
