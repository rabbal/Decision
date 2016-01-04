using System.Collections.Generic;

namespace Decision.ViewModel.TrainingCourse
{
    public class TrainingCourseListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public TrainingCourseSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش دوره آموزشی
        /// </summary>
        public IEnumerable<TrainingCourseViewModel> TrainingCourses { get; set; }
    }
}