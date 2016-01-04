using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.TrainingCenter
{
    public class TrainingCenterViewModel : BaseViewModel
    {
        /// <summary>
        /// آی دی مرکز کار آموزی
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// نام مرکز کار آموزی
        /// </summary>
        [DisplayName("نام")]
        public  string CenterName { get; set; }
        /// <summary>
        /// نشانی
        /// </summary>
        [DisplayName("نشانی")]
       
        public  string Location { get; set; }
        /// <summary>
        /// شماره تلفن 1
        /// </summary>
        [DisplayName("شماره تلفن 1")]
        public  string PhoneNumber1 { get; set; }
        /// <summary>
        /// شماره تلفن 2
        /// </summary>
        [DisplayName("شماره تلفن 2")]
        public  string PhoneNumber2 { get; set; }
        /// <summary>
        /// توضیحات در صورت نیاز
        /// </summary>
        [DisplayName("توضیحات")]
        public  string Description { get; set; }
        /// <summary>
        ///  شهر
        /// </summary>
        [DisplayName("شهر")]
        public string City { get; set; }
        /// <summary>
        ///  استان
        /// </summary>
        [DisplayName("استان")]
        public string  State { get; set; }
    }
}