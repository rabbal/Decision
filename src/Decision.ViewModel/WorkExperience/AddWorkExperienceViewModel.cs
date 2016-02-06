using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.ApplicantInfo;

namespace Decision.ViewModel.WorkExperience
{
    /// <summary>
    /// ویومدل مربوط به درج سابقه کاری
    /// </summary>
    public class AddWorkExperienceViewModel
    {
        #region Ctor
        public AddWorkExperienceViewModel()
        {
            Cities = new List<SelectListItem>();
            TenureBeginDate = TenureEndDate = DateTime.Now;
        }
        #endregion

        #region Properties

        /// <summary>
        /// زمان آغاز به‌کار
        /// </summary>
        [DisplayName("زمان آغاز به‌کار")]
        public  DateTime TenureBeginDate { get; set; }

        /// <summary>
        /// زمان پایان ‌کار
        /// </summary>
        [DisplayName("زمان پایان ‌کار")]
        public  DateTime TenureEndDate { get; set; }

        /// <summary>
        /// تعداد طرحهای متوقف‌شده
        /// </summary>
        [DisplayName("تعداد طرحهای متوقف‌شده")]
        public  int ReferentialProjectCount { get; set; }

        /// <summary>
        /// تعداد طرحهای انجام‌شده
        /// </summary>
        [DisplayName("تعداد طرحهای انجام‌شده")]
        public  int ClosedProjectCount { get; set; }

        /// <summary>
        /// تعداد طرحهای جاری
        /// </summary>
        [DisplayName("تعداد طرحهای جاری")]
        public  int OpenProjectCount { get; set; }

        /// <summary>
        /// نوع مشارکت
        /// </summary>
        [DisplayName("نوع مشارکت")]
        public  CooperationType CooperationType { get; set; }

        /// <summary>
        /// اداره محل خدمت
        /// </summary>
        [Required(ErrorMessage = "لطفا محل خدمت را پر کنید")]
        [StringLength(1024, ErrorMessage = "اداره محل خدمت باید بین دو تا ۱۰۲۴ کاراکتر باشد")]
        [DisplayName("اداره محل خدمت")]
        public  string OfficeName { get; set; }
        /// <summary>
        /// آی دی شهر محل خدمت
        /// </summary>
        [Required(ErrorMessage = "لطفا شهر محل خدمت را انتخاب کنید")]
        [DisplayName("شهر")]
        public  string City { get; set; }
        /// <summary>
        /// آی دی شهر محل خدمت
        /// </summary>
        [Required(ErrorMessage = "لطفا استان محل خدمت را انتخاب کنید")]
        [DisplayName("استان")]
        public string State { get; set; }
        /// <summary>
        /// آی دی عنوان پست سازمانی
        /// </summary>
        [Required(ErrorMessage = "لطفا پست سازمانی را انتخاب کنید")]
        [DisplayName("پست سازمانی")]
        public  Guid TitleId { get; set; }
        [Required]
        public Guid ApplicantId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست استان ها برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> States { get; set; }
        /// <summary>
        /// لیست شهر ها برای لیست آبشاری
        /// </summary>
        public IEnumerable<SelectListItem> Cities { get; set; }
        /// <summary>
        /// لیست عنوان ها برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> Titles { get; set; }
        #endregion 
    }
}