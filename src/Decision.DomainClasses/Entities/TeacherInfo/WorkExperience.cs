using System;

using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    ///نشان دهنده سابقه کاری استاد
    /// </summary>
    public class WorkExperience : BaseEntity
    {
       
        #region Properties
       
        /// <summary>
        /// زمان آغاز به‌کار
        /// </summary>
        public  DateTime TenureBeginDate { get; set; }
        /// <summary>
        /// زمان پایان ‌کار
        /// </summary>
        public  DateTime TenureEndDate { get; set; }
        /// <summary>
        /// تعداد طرحهای مردود
        /// </summary>
        public  int ReferentialProjectCount { get; set; }
        /// <summary>
        /// تعداد طرحهای پایان یافته
        /// </summary>
        public  int ClosedProjectCount { get; set; }
        /// <summary>
        /// تعداد طرحهای جاری
        /// </summary>
        public  int OpenProjectCount { get; set; }
        /// <summary>
        /// نوع مشارکت
        /// </summary>
        public CooperationType CooperationType { get; set; }
        /// <summary>
        /// اداره محل خدمت
        /// </summary>
        public  string OfficeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public  string State { get; set; }
        #endregion

        #region NavigationProperties
       
        /// <summary>
        /// استاد مربوط به این سابقه کاری
        /// </summary>
        public  Teacher Teacher { get; set; }
        /// <summary>
        /// آی دی استاد مربوط به این سابقه کاری
        /// </summary>
        public  Guid TeacherId { get; set; }
        /// <summary>
        /// عنوان پست سازمانی
        /// </summary>
        public  Title Title { get; set; }
        /// <summary>
        /// آی دی عنوان پست سازمانی
        /// </summary>
        public  Guid TitleId { get; set; }
        #endregion
    }
}
