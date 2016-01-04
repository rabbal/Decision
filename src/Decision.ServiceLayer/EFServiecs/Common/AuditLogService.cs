using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using AutoMapper;
using Decision.Common.Helpers.Extentions;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Contracts.Users;
using Decision.ViewModel.Home;
using Microsoft.AspNet.Identity;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    /// <summary>
    /// ارائه دهنده سرویس لاگ آماری
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IDbSet<AuditLog> _logs;
        #endregion

        #region Ctor
        public AuditLogService(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IMappingEngine mappingEngine)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logs = _unitOfWork.Set<AuditLog>();
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region Create
        public void Create(string description, AuditLogType type)
        {
            var log = new AuditLog
            {
                Description = description,
                CreatorId =_userManager.GetCurrentUserId(),
                Type = type
            };

            switch (type)
            {
                case AuditLogType.JustDescription:
                    break;
                case AuditLogType.Serialize:
                    log.NewValue = _unitOfWork.AuditNewValue;
                    log.OldValue = _unitOfWork.AuditOldValue;
                    log.RecordedEntityId = _unitOfWork.RecordedEntityKey;
                    break;
            }
            _logs.Add(log);
            _unitOfWork.SaveChanges();
        }
        public void Create(string description, string propertyValue)
        {
            if (!propertyValue.HasValue()) Create(description, AuditLogType.JustDescription);
            var log = new AuditLog
            {
                Description =string.Format("{0} با مشخصه {1}",description,propertyValue),
                CreatorId = Guid.Parse(HttpContext.Current.User.Identity.GetUserId()),
                Type = AuditLogType.JustDescription
            };
            _logs.Add(log);
            _unitOfWork.SaveChanges();
        }

        public IList<LastActivityViewModel> GetLastActivities()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
