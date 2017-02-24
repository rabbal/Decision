using System;
using Decision.DomainClasses.ApplicantInfo;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Evaluations
{
    /// <summary>
    /// نشان دهنده مصاحبه ای که با متقاضی شده است
    /// </summary>
    public class Interview : BaseEntity
    {
        #region Properties

        /// <summary>
        /// تاریخ مصاحبه
        /// </summary>
        public DateTime InterviewDate { get; set; }
        /// <summary>
        /// متن کامل مصاحبه
        /// </summary>
        public string Body { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// آی دی متقاضی مصاحبه شده
        /// </summary>
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// متقاضی مصاحبه شده
        /// </summary>
        public Applicant Applicant { get; set; }
        #endregion
    }
}
