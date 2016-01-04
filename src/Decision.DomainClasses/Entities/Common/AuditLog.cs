using System;
using System.Xml.Linq;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// نشان دهنده لاگ  فعالت های کاربر
    /// </summary>
    public class AuditLog
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public AuditLog()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            OperateDate = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// نوع لاگ
        /// <remarks>قابل سریالایز باشد یا ...</remarks>
        /// </summary>
        public  AuditLogType Type { get; set; }
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// نام جدولی که این لاگ مربوط به آن است
        /// </summary>
        public  string TableName { get; set; }
        /// <summary>
        /// آی دی رکوردی که لاگ آن گرفته شده است
        /// </summary>
        public  Guid? RecordedEntityId { get; set; }
        /// <summary>
        /// توضیحات کلی لاگ
        /// </summary>
        public  string Description { get; set; }

        /// <summary>
        /// مقدار بعد از تغییر فیلد ها
        /// </summary>
        public  string NewValue { get; set; }

        /// <summary>
        ///  ساختار ایکس ام ال مقدار بعد از تغییر فیلد ها
        /// </summary>
        public  XElement XmlNewValue
        {
            get { return XElement.Parse(NewValue); }
            set { NewValue = value.ToString(); }
        }

        /// <summary>
        /// مقدار قبل از تغییر فیلد ها
        /// </summary>
        public  string OldValue { get; set; }

        /// <summary>
        ///  ساختار ایکس ام ال مقدار قبل از تغییر فیلد ها
        /// </summary>
        public  XElement XmlOldValue
        {
            get { return XElement.Parse(OldValue); }
            set { OldValue = value.ToString(); }
        }
        /// <summary>
        /// تاریخ انجام عملیات 
        /// </summary>
        public  DateTime OperateDate { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// کاربر ایجاد کننده 
        /// </summary>
        public  User Creator { get; set; }
        /// <summary>
        /// آی دی کاربر ایجاد کننده
        /// </summary>
        public  Guid CreatorId { get; set; }

        #endregion
    }
}
