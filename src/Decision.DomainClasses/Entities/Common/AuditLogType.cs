using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// نشان دهنده انواع لاگ های سیستم میباشد
    /// </summary>
    public enum  AuditLogType
    {
        /// <summary>
        /// سریالایز شود
        /// </summary>
        Serialize,
        /// <summary>
        /// فقط توضیحات کلی ذخیره شود
        /// </summary>
        JustDescription,
        /// <summary>
        /// لاگ های مربوط به ورود 
        /// </summary>
        Login,
        /// <summary>
        /// خروج
        /// </summary>
        LogOut

    }
}
