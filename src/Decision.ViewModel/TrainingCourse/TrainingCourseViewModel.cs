using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCourse
{
    public class TrainingCourseViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی دوره آموزشی
        /// </summary>
        public  Guid Id { get; set; }

        /// <summary>
        /// کدی برای شناسایی دوره
        /// </summary>
        [DisplayName("کد دوره")]
        public  string CourseCode { get; set; }

        /// <summary>
        /// تاریخ آغاز دوره
        /// </summary>
        [DisplayName("تاریخ شروع")]
        public  DateTime BeginDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره
        /// </summary>
        [DisplayName("تاریخ پایان")]
        public  DateTime EndDate { get; set; }

        /// <summary>
        /// مجموع ساعت آموزشی نظری
        /// </summary>
        [DisplayName("مدت آموزش نظری")]
        public  int TheoryTotalHoures { get; set; }

        /// <summary>
        /// مجموع ساعت آموزشی عملی
        /// </summary>
        [DisplayName("مدت آموزش عملی")]
        public  int PracticalTotalHoures { get; set; }

        /// <summary>
        /// آی دی مرکز کار آموزی
        /// </summary>
        [DisplayName("مرکز کارآموزی")]
        public Guid TrainingCenterId { get; set; }
        #endregion 
    }
}