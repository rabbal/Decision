using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Interview
{
    /// <summary>
    /// ویومدل نمایش مصاحبه
    /// </summary>
    public class InterviewViewModel :BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی مصاحبه
        /// </summary>
        public  Guid Id { get; set; }
        public Guid ApplicantId { get; set; }

        /// <summary>
        /// تاریخ مصاحبه
        /// </summary>
        [DisplayName("تاریخ مصاحبه")]
        public  DateTime InterviewDate { get; set; }

        /// <summary>
        /// متن کامل مصاحبه
        /// </summary>
        [DisplayName("متن مصاحبه")]
        public  string Body { get; set; }

        /// <summary>
        /// خلاصه ای از مصاحبه
        /// </summary>
        [DisplayName("خلاصه")]
        public  string Brief { get; set; }

        /// <summary>
        /// آی دی مصاحبه کننده ها
        /// </summary>
        [DisplayName("مصاحبه کننده")]
        public  string InterviewerName { get; set; }
        #endregion
    }
}