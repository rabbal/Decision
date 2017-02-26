using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.WorkExperience
{
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

        [DisplayName("زمان آغاز به‌کار")]
        public  DateTime TenureBeginDate { get; set; }

        [DisplayName("زمان پایان ‌کار")]
        public  DateTime TenureEndDate { get; set; }

        [DisplayName("تعداد طرحهای متوقف‌شده")]
        public  int ReferentialProjectCount { get; set; }

        [DisplayName("تعداد طرحهای انجام‌شده")]
        public  int ClosedProjectCount { get; set; }

        [DisplayName("تعداد طرحهای جاری")]
        public  int OpenProjectCount { get; set; }

        [DisplayName("نوع مشارکت")]
        public  CooperationType CooperationType { get; set; }

        [Required(ErrorMessage = "لطفا محل خدمت را پر کنید")]
        [StringLength(1024, ErrorMessage = "اداره محل خدمت باید بین دو تا ۱۰۲۴ کاراکتر باشد")]
        [DisplayName("اداره محل خدمت")]
        public  string OfficeName { get; set; }
        [Required(ErrorMessage = "لطفا شهر محل خدمت را انتخاب کنید")]
        [DisplayName("شهر")]
        public  string City { get; set; }
        [Required(ErrorMessage = "لطفا استان محل خدمت را انتخاب کنید")]
        [DisplayName("استان")]
        public string State { get; set; }
        [Required(ErrorMessage = "لطفا پست سازمانی را انتخاب کنید")]
        [DisplayName("پست سازمانی")]
        public  Guid TitleId { get; set; }
        [Required]
        public Guid ApplicantId { get; set; }
        #endregion

        #region SelectListItems
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Titles { get; set; }
        #endregion 
    }
}