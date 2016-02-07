using System.Xml.Linq;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// نشان دهنده تنظیمات برنامه
    /// </summary>
    public class Setting
    {
        #region Properties
        /// <summary>
        /// نام تنظیمات
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// محتوای تنظیمات
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// برای مباحث همزمانی
        /// </summary>
        public byte[] RowVersion { get; set; }
        #endregion
    }
}
