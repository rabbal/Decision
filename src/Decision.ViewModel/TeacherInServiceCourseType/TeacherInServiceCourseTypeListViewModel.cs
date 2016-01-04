using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ViewModel.TeacherInServiceCourseType
{
    /// <summary>
    /// ویومدل نمایش لیست تعداد ساعت انواع ضمن خدمت برای استاد
    /// </summary>
    public class TeacherInServiceCourseTypeListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public TeacherInServiceCourseTypeSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش تعداد ساعت انواع ضمن خدمت برای استاد
        /// </summary>
        public IEnumerable<TeacherInServiceCourseTypeViewModel> TeacherInServiceCourseTypes { get; set; }

        /// <summary>
        /// لیست عنوان دوره های ضمن خدمت برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> InServiceCourseTypeTitles { get; set; }
    }
}