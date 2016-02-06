using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.WorkExperience
{
    /// <summary>
    /// ویومدل نمایش سابقه کاری
    /// </summary>
    public class WorkExperienceViewModel:BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی سابقه کاری
        /// </summary>
        public  Guid Id { get; set; }

        public Guid ApplicantId { get; set; }
        /// <summary>
        /// زمان آغاز به‌کار
        /// </summary>
        [DisplayName("زمان آغاز به‌کار")]
        public  DateTime TenureBeginDate { get; set; }

        /// <summary>
        /// زمان پایان ‌کار
        /// </summary>
        [DisplayName("زمان پایان ‌کار")]
        public  DateTime TenureEndDate { get; set; }

        /// <summary>
        /// تعداد طرحهای متوقف‌شده
        /// </summary>
        [DisplayName("تعداد طرحهای متوقف‌شده")]
        public  int ReferentialProjectCount { get; set; }

        /// <summary>
        /// تعداد طرحهای انجام‌شده
        /// </summary>
        [DisplayName("تعداد طرحهای انجام‌شده")]
        public  int ClosedProjectCount { get; set; }

        /// <summary>
        /// تعداد طرحهای جاری
        /// </summary>
        [DisplayName("تعداد طرحهای جاری")]
        public  int OpenProjectCount { get; set; }

        /// <summary>
        /// نوع مشارکت
        /// </summary>
        [DisplayName("نوع مشارکت")]
        public  CooperationType CooperationType { get; set; }

        /// <summary>
        /// اداره محل خدمت
        /// </summary>
        [DisplayName("اداره محل خدمت")]
        public  string OfficeName { get; set; }
        
        /// <summary>
        /// شهر محل خدمت
        /// </summary>
        [DisplayName("شهر")]
        public  string City { get; set; }
        /// <summary>
        /// شهر محل خدمت
        /// </summary>
        [DisplayName("استان")]
        public string State { get; set; }

        /// <summary>
        /// عنوان پست سازمانی
        /// </summary>
        [DisplayName("پست سازمانی")]
        public  string TitleName { get; set; }
        #endregion
    }
}