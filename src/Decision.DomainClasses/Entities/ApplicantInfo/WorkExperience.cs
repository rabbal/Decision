using System;

using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{
    /// <summary>
    ///نشان دهنده سابقه کاری متقاضی
    /// </summary>
    public class WorkExperience : BaseEntity
    {
        #region Properties
        /// <summary>
        /// امتیاز 
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// زمان آغاز به‌کار
        /// </summary>
        public DateTime TenureBeginDate { get; set; }
        /// <summary>
        /// زمان پایان ‌کار
        /// </summary>
        public DateTime TenureEndDate { get; set; }
        /// <summary>
        /// اداره محل خدمت
        /// </summary>
        public string OfficeName { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// آدرس محل کار
        /// </summary>
        public string OffieceAddress { get; set; }
        /// <summary>
        /// تلفن محل کار
        /// </summary>
        public string OfficePhoneNumber { get; set; }
        /// <summary>
        /// نوع مسئولیت
        /// </summary>
        public string ResponsibilityType { get; set; }
        /// <summary>
        /// واحد سازمانی
        /// </summary>
        public string OrganizationUnit { get; set; }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// متقاضی مربوط به این سابقه کاری
        /// </summary>
        public Applicant Applicant { get; set; }
        /// <summary>
        /// آی دی متقاضی مربوط به این سابقه کاری
        /// </summary>
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
