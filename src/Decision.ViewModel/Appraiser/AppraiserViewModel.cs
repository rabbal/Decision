using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.Appraiser
{
    /// <summary>
    /// ویومدل نمایش ارزیاب - ارزیاب، مصاحبه کننده و غیره
    /// </summary>
    public class AppraiserViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی ارزیاب
        /// </summary>
        public  Guid Id { get; set; }

        /// <summary>
        /// نام ارزیاب
        /// </summary>
        [DisplayName("نام")]
        public  string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی ارزیاب
        /// </summary>
        [DisplayName("نام خانوادگی")]
        public  string LastName { get; set; }
        /// <summary>
        /// شماره همراه ارزیاب
        /// </summary>
        [DisplayName("شماره همراه")]
        public  string CellPhone { get; set; }

        /// <summary>
        /// جنسیت ارزیاب
        /// </summary>
        [DisplayName("جنسیت")]
        public  GenderType Gender { get; set; }

        /// <summary>
        /// عنوان  ارزیاب  
        /// مهندس/دکتر/...
        /// </summary>
        [DisplayName("عنوان")]
        public  string AppraiserTitleName { get; set; }
        #endregion 
    }
}