using System;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده ارزیابی از  متقاضی
    /// </summary>
    public class EntireEvaluation : BaseEntity
    {
        #region Properties
        /// <summary>
        ///  نظریه کلی برای متقاضی
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// تاریخ ارزیابی
        /// </summary>
        public DateTime EvaluationDate { get; set; }
        /// <summary>
        /// نقاط ضعف متقاضی
        /// </summary>
        public string Foible { get; set; }
        /// <summary>
        /// نقطه قوت متقاضی
        /// </summary>
        public string StrongPoint { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی متقاضی ارزیابی شده
        /// </summary>
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// متقاضی ارزیابی شده
        /// </summary>
        public Applicant Applicant { get; set; }
        #endregion
    }
}
