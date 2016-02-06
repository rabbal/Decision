using System;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    /// مشخص کننده معرفان
    /// </summary>
    public class Presenter : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// نوع رابطه و نحوه آشنایی
        /// </summary>
        public string RelationType { get; set; }
        /// <summary>
        /// شماره ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        public string CellPhoneNumber { get; set; }
        /// <summary>
        /// مدت آشنایی
        /// </summary>
        public string DurationOfRelation { get; set; }
        /// <summary>
        /// شغل شخص
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// نوع معرف 
        /// </summary>
        public PresenterType Type { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// متقاضی 
        /// </summary>
        public Applicant Applicant { get; set; }
        /// <summary>
        /// آی دی متقاضی
        /// </summary>
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
