using System.Data.Entity;
using System.Reflection;
using Decision.DataLayer.Context;
using Decision.DomainClasses.Entities.Common;

namespace Decision.ServiceLayer.Settings
{
    public abstract class SettingsBase
    {
        #region Fields

        private readonly IDbSet<Setting> _settings;
        private readonly string _name;
        private readonly PropertyInfo[] _properties;
        #endregion

        #region Ctor
        protected SettingsBase(IUnitOfWork unitOfWork)
        {
            _settings = unitOfWork.Set<Setting>();

            var type = GetType();
            _name = type.Name;
            _properties = type.GetProperties();
        }
        #endregion

        #region Load
        public virtual void Load()
        {
            //var settings = _settings.Where(w => w.Type == _name).Cacheable().ToList();

            //foreach (var propertyInfo in _properties)
            //{
            //    var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
            //    if (setting != null)
            //    {
            //        propertyInfo.SetValue(this, Convert.ChangeType(setting.Value, propertyInfo.PropertyType));
            //    }
            //}
        }
        #endregion

        #region
        public virtual void Save()
        {
            //var settings = _settings.Where(w => w.Type == _name).Cacheable().ToList();

            //foreach (var propertyInfo in _properties)
            //{
            //    var propertyValue = propertyInfo.GetValue(this, null);
            //    var value = (propertyValue == null) ? null : propertyValue.ToString();

            //    var setting = settings.SingleOrDefault(s => s.Name == propertyInfo.Name);
            //    if (setting != null)
            //    {
            //        setting.Value = value;
            //    }
            //    else
            //    {
            //        var newSetting = new Setting
            //        {
            //            Name = propertyInfo.Name,
            //            Type = _name,
            //            Value = value,
            //        };

            //        _settings.Add(newSetting);
            //    }
            //}
        }
        #endregion

    }
}
