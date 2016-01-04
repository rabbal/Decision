using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EntireEvaluation
{
    /// <summary>
    /// ویومدل نمایش ارزیابی از استاد
    /// </summary>
    public class EntireEvaluationViewModel :BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی ارزیابی از استاد
        /// </summary>
        public  Guid Id { get; set; }
        public Guid TeacherId { get; set; }

        /// <summary>
        ///  نظریه کلی برای استاد
        /// </summary>
        [DisplayName("متن ارزیابی")]
        public  string Content { get; set; }

        /// <summary>
        /// تاریخ ارزیابی
        /// </summary>
        [DisplayName("تاریخ")]
        public  DateTime EvaluationDate { get; set; }

        /// <summary>
        /// خلاصه ارزیابی 
        /// </summary>
        [DisplayName("خلاصه ارزیابی")]
        public  string Brief { get; set; }

        /// <summary>
        /// نقاط ضعف استاد
        /// </summary>
        [DisplayName("نقاط ضعف")]
        public  string Foible { get; set; }

        /// <summary>
        /// نقطه قوت استاد
        /// </summary>
        [DisplayName("نقاط قوت")]
        public  string StrongPoint { get; set; }

        /// <summary>
        /// آی دی ارزیاب
        /// </summary>
        [DisplayName("ارزیاب")]
        public  string EvaluatorName { get; set; }
        #endregion
    }
}