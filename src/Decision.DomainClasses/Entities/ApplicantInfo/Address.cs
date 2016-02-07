using System;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.ApplicantInfo
{

    /// <summary>
    ///نشان دهنده آدرس 
    /// </summary>
    public class Address : BaseEntity
    {
        #region Properties
        /// <summary>
        /// شماره همراه مربوط به آدرس
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// نشانی کامل 
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// آدرس محل کار است یا محل زندگی
        /// </summary>
        public AddressType Type { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string PostalCode { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// متقاضی صاحب آدرس
        /// </summary>
        public Applicant Applicant { get; set; }
        /// <summary>
        ///آی دی متقاضی صاحب آدرس 
        /// </summary>
        public Guid ApplicantId { get; set; }
        #endregion
    }
}
