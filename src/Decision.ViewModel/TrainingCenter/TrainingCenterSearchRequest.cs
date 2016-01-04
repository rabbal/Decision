using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCenter
{
    /// <summary>
    /// کلاسی برای کپسوله کردن اطلاعات جستجو برای مراکز کارآموزی
    /// </summary>
    public class TrainingCenterSearchRequest : BaseSearchRequest
    {
        /// <summary>
        /// آی دی شهر
        /// </summary>
        [DisplayName("شهر")]
        public string City { get; set; }
        /// <summary>
        /// آی دی استان
        /// </summary>
        [DisplayName("استان")]
        public string State { get; set; }

    }
}