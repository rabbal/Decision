using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل ویرایش سابقه آموزشی
    /// </summary>
    public class EditEducationalExperienceViewModel:BaseRowVersion
    {
        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public EditEducationalExperienceViewModel()
        {
            Titles = new List<SelectListItem>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی سابقه آموزشی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// فیلد مشترک برای موارد زیر
        /// <remarks>نام سازمان مربوطه/توضیحات/نام مرکز علمی</remarks>
        /// </summary>

        public string Description { get; set; }

        ///<summary>
        /// سال آغاز  
        /// </summary>
        [DisplayName("سال آغاز")]
        [Required(ErrorMessage = "لطفا سال آغاز را وارد کنید")]
        public int BeginYear { get; set; }

        /// <summary>
        /// سال پایان 
        /// </summary>
        [DisplayName("سال پایان")]
        [Required(ErrorMessage = "لطفا سال پایان را وارد کنید")]
        public int EndYear { get; set; }

        /// <summary>
        /// آی دی متقاضی مرتبط با سابقه تدریس
        /// </summary>
        [Required]
        public Guid ApplicantId { get; set; }

        /// <summary>
        ///آی دی عنوان تدریس شده یا درس
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان درس را نتخاب کنید")]
        [DisplayName("عنوان درس")]
        public Guid TitleId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست عنوان ها برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> Titles { get; set; }
        #endregion
    }
}