using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ViewModel.ApplicantInServiceCourseType
{
    /// <summary>
    /// ویومدل نمایش لیست تعداد ساعت انواع ضمن خدمت برای متقاضی
    /// </summary>
    public class ApplicantInServiceCourseTypeListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public ApplicantInServiceCourseTypeSearchRequest SearchRequest { get; set; }

        /// <summary>
        /// لیست ویو مدل نمایش تعداد ساعت انواع ضمن خدمت برای متقاضی
        /// </summary>
        public IEnumerable<ApplicantInServiceCourseTypeViewModel> ApplicantInServiceCourseTypes { get; set; }

        /// <summary>
        /// لیست عنوان دوره های ضمن خدمت برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> InServiceCourseTypeTitles { get; set; }
    }
}