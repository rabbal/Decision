using System;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility.Attributes;

namespace Decision.DomainClasses.Entities.TeacherInfo
{

    /// <summary>
    ///نشان دهنده آدرس 
    /// </summary>
    [TrackChanges]
    [Alias("آدرس")]
    public class Address : BaseEntity
    {
        #region Properties
        /// <summary>
        /// شماره همراه مربوط به آدرس
        /// </summary>
        [Alias("شماره همراه")]
        public string CellPhone { get; set; }
        /// <summary>
        /// نشانی کامل 
        /// </summary>
        [Alias("نشانی")]
        public string Location { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        [Alias("شماره تلفن")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// آدرس محل کار است یا محل زندگی
        /// </summary>
        [Alias("نوع آدرس")]
        public AddressType Type { get; set; }
        /// <summary>
        /// شهر
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// استان
        /// </summary>
        public string State { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// استاد صاحب آدرس
        /// </summary>
        public Teacher Teacher { get; set; }
        /// <summary>
        ///آی دی استاد صاحب آدرس 
        /// </summary>
        public Guid TeacherId { get; set; }

        #endregion
    }
}
