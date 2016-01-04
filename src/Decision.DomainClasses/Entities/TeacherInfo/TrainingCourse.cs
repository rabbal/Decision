using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.TeacherInfo
{
    /// <summary>
    /// نشان دهنده دوره کار آموزی
    /// </summary>
    public class TrainingCourse : BaseEntity
    {
       
        #region Properties
        /// <summary>
        /// کدی برای شناسایی دوره
        /// </summary>
        public  string CourseCode { get; set; }
        /// <summary>
        /// تاریخ آغاز دوره
        /// </summary>
        public  DateTime BeginDate { get; set; }
        /// <summary>
        /// تاریخ پایان دوره
        /// </summary>
        public  DateTime EndDate { get; set; }
        /// <summary>
        /// مجموع ساعت آموزشی نظری
        /// </summary>
        public  int TheoryTotalHoures { get; set; }
        /// <summary>
        /// مجموع ساعت آموزشی عملی
        /// </summary>
        public  int PracticalTotalHoures { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// مرکز کار آموزی
        /// </summary>
        public  TrainingCenter TrainingCenter { get; set; }
        /// <summary>
        /// آی دی مرکز کار آموزی
        /// </summary>
        public  Guid TrainingCenterId { get; set; }
        /// <summary>
        /// شرکت کنندگان در دوره 
        /// </summary>
        public  ICollection<Teacher> Teachers { get; set; }
        #endregion
    }
}
