using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Address
{
    public class AddressViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی آدرس
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// نام استان
        /// </summary>
        [DisplayName("استان")]
        public string State { get; set; }

        /// <summary>
        /// نام شهر
        /// </summary>
        [DisplayName("شهر")]
        public string City { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        [DisplayName("شماره همراه")]
        public string CellPhone { get; set; }

        /// <summary>
        /// نشانی
        /// </summary>
        [DisplayName("نشانی")]
        public string Location { get; set; }

        /// <summary>
        /// تلفن ثابت
        /// </summary>
        [DisplayName("تلفن ثابت")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// آدرس محل کار است یا محل زندگی
        /// </summary>
        public AddressType Type { get; set; }

        public Guid ApplicantId { get; set; }

        #endregion
    }
}