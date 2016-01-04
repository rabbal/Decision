using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده مصاحبه ای که با استاد شده است
    /// </summary>
    public class Interview : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Interview()
        {
            Attachment = BitConverter.GetBytes(0);
        }
        #region Properties
        /// <summary>
        /// تاریخ مصاحبه
        /// </summary>
        public  DateTime InterviewDate { get; set; }
        /// <summary>
        /// متن کامل مصاحبه
        /// </summary>
        public  string Body { get; set; }
        /// <summary>
        /// خلاصه ای از مصاحبه
        /// </summary>
        public  string Brief { get; set; }
        /// <summary>
        /// فایل ضمیمه مصاحبه
        /// </summary>
        public  byte[] Attachment { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی استاد مصاحبه شده
        /// </summary>
        public  Guid TeacherId { get; set; }
        /// <summary>
        /// استاد مصاحبه شده
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// مصاحبه کننده ها
        /// </summary>
        public  Appraiser Interviewer { get; set; }

        /// <summary>
        /// آی دی مصاحبه کننده ها
        /// </summary>
        public  Guid InterviewerId { get; set; }

        #endregion
    }
}
