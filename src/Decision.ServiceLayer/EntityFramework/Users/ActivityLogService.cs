using System;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using Decision.DataLayer.Context;
using Decision.ServiceLayer.Interfaces.Identity;
using Decision.ViewModel.Home;

namespace Decision.ServiceLayer.EntityFramework.Users
{
    public class ActivityLogService : IActivityLogService
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<ActivityLog> _logs;
        #endregion

        #region Ctor
        public ActivityLogService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logs = _unitOfWork.Set<ActivityLog>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Create
        public void Create(ActivityLog log)
        {
            _logs.Add(log);
        }
        #endregion

        public IList<LastActivityViewModel> GetLastActivities()
        {
            throw new NotImplementedException();
        }

    }
}
