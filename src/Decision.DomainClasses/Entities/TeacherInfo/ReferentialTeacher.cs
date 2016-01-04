using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.Users;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده این است که کدام استاد
    ///  توسط چه مدیری به چه کاربر ارجاع داده شده است
    /// </summary>
    public class ReferentialTeacher
    {
        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public ReferentialTeacher()
        {
            CreateDate=DateTime.Now;
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی رکورد به عنوان کلید اصلی
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// تاریخ ایجاد این ارجاع
        /// </summary>
        public  DateTime CreateDate { get; set; }
        /// <summary>
        /// تاریخی که این ارجاع تکمیل شده است
        /// <remarks>وقتی کاربر مورد نظر اصلاحات لازم را انجام داد و کلید تکمیل را  فشرد</remarks>
        /// </summary>
        public  DateTime? FinishedDate { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی  دی کاربری که این استاد را ارجاع داده است
        /// </summary>
        public  Guid ReferencedFromId { get; set; }
        /// <summary>
        ///  کاربری که این استاد را ارجاع داده است
        /// </summary>
        public  User ReferencedFrom { get; set; }

        /// <summary>
        /// آی  دی کاربری که این استاد برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        public  Guid ReferencedToId { get; set; }
        /// <summary>
        /// کاربری که این استاد برای اصلاح به او ارجاع داده  شده است
        /// </summary>
        public  User ReferencedTo { get; set; }
        /// <summary>
        /// استاد ارجاع داده شده
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        ///  آی دی استاد ارجاع داده شده
        /// </summary>
        public  Guid TeacherId { get; set; }
        #endregion
    }
}
