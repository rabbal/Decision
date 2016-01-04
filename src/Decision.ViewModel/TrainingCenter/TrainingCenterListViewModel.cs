using System.Collections.Generic;
using System.Web.Mvc;

namespace Decision.ViewModel.TrainingCenter
{
    public class TrainingCenterListViewModel
    {
        /// <summary>
        /// اطلاعات جستجو و مرتب سازی
        /// </summary>
        public TrainingCenterSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// لیست ویو مدل نمایش مرکز کارآموزی ها
        /// </summary>
        public IEnumerable<TrainingCenterViewModel> TrainingCenters { get; set; }
        /// <summary>
        /// لیست استان ها برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> States { get; set; }
        /// <summary>
        /// لیست شهر ها برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}