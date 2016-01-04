using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;
using Decision.ServiceLayer.Contracts.Common;
using Decision.ServiceLayer.Settings;

namespace Decision.ServiceLayer.EFServiecs.Common
{
    public class SettingService : ISettingService
    {
        #region Fields
        #endregion

        #region Ctor
        public SettingService(IUnitOfWork unitOfWork)
        {

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

        public Task<bool> SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadSetting<T>() where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public Task SetSetting<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task SaveSetting<T>(T settings) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public Task SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
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

        public Task DeleteSetting<T>() where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public Task DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
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
