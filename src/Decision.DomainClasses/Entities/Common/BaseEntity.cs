using System;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Common
{
    /// <summary>
    /// کلاس پایه
    /// </summary>
    public abstract class BaseEntity
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        protected BaseEntity()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
            CreateDate = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی موجودیت 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// مورد استفاده باری حذف نرم 
        /// for soft delete
        /// </summary>
        public  bool IsDeleted { get; set; }
        /// <summary>
        /// برای جلوگیری از مسائل همزمانی
        /// </summary>
        public  byte[] RowVersion { get; set; }
        /// <summary>
        /// تاریخ درج 
        /// </summary>
        public  DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخ حذف نرم رکورد
        /// </summary>
        public  DateTime? SoftDeleteDate { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public  DateTime? LastModifiedDate { get; set; }
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
        /// <summary>
        /// آخرین کاربر ویرایش کننده
        /// </summary>
        public  User LasModifier { get; set; }
        /// <summary>
        /// آی دی آخرین ویرایش کننده
        /// </summary>
        public  Guid? LasModifierId { get; set; }
        #endregion
    }
}
