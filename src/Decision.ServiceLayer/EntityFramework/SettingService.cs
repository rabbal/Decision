using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Decision.DomainClasses;
using Decision.Framework.Domain.Entities;
using Decision.Framework.Domain.Uow;
using Decision.ServiceLayer.Interfaces;

namespace Decision.ServiceLayer.EntityFramework
{
    public class SettingService : ISettingService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public SettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion


        public Task<Setting> GetSettingById(int settingId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setting>> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadSetting<T>() where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public Task SetSetting<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task SaveSetting<T>(T settings) where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public Task SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public void InsertSetting(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSetting(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSetting(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSetting<T>() where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISetting, new()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSetting(string key)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSettings(string rootKey)
        {
            throw new NotImplementedException();
        }

        public Task ClearCache()
        {
            throw new NotImplementedException();
        }
    }
}
