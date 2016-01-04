using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCourse
{
    public class EditTrainingCourseViewModel : BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی دوره آموزشی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// کدی برای شناسایی دوره
        /// </summary>
        [Required(ErrorMessage = "لطفا کد دوره را وارد کنید")]
        [DisplayName("کد دوره")]
        [StringLength(256, ErrorMessage = "کد دوره باید حداکثر ۲۵۶ کاراکتر باشد")]
        public string CourseCode { get; set; }


        /// <summary>
        /// تاریخ آغاز دوره
        /// </summary>
        [DisplayName("تاریخ شروع")]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره
        /// </summary>
        [DisplayName("تاریخ پایان")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// مجموع ساعت آموزشی نظری
        /// </summary>
        [DisplayName("مدت آموزش نظری")]
        public int TheoryTotalHoures { get; set; }

        /// <summary>
        /// مجموع ساعت آموزشی عملی
        /// </summary>
        [DisplayName("مدت آموزش عملی")]
        public int PracticalTotalHoures { get; set; }

        /// <summary>
        /// آی دی مرکز کار آموزی
        /// </summary>
        [DisplayName("مرکز کارآموزی")]
        [Required(ErrorMessage = "لطفا مرکز کارآموزی مربوطه را انتخاب کنید")]
        public Guid TrainingCenterId { get; set; }

        #endregion
    }
}