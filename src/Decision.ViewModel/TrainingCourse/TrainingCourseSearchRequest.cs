using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCourse
{
    public class TrainingCourseSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// تاریخ آغاز
        /// </summary>

        [DisplayName("از تاریخ")]
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// تاریخ پایان
        /// </summary>
        [DisplayName("تا تاریخ")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// آی دی مرکز کارآموزی برای واکشی دوره های آن
        /// </summary>
        public Guid TrainingCenterId { get; set; }

    }

    public static class TrainingCourseSortBy
    {
        public const string BeginDate = "BeginDate";
        public const string EndDate = "EndDate";
        public const string PracticalTotalHoures = "PracticalTotalHoures";
        public const string TheoryTotalHoures = "TheoryTotalHoures";
    }
}