using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using Decision.Common.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.Home;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Users
{
    /// <summary>
    /// ارائه دهنده سرویس لاگ آماری
    /// </summary>
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
