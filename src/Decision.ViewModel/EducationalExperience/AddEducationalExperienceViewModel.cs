using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل درج ساقه آموزشی
    /// </summary>
    public class AddEducationalExperienceViewModel
    {
        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AddEducationalExperienceViewModel()
        {
            Titles = new List<SelectListItem>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// فیلد مشترک برای موارد زیر
        /// <remarks>نام سازمان مربوطه/توضیحات/نام مرکز علمی</remarks>
        /// </summary>
        
        public  string Description { get; set; }

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
        /// نوع سابقه آموزشی
        /// </summary>
        [DisplayName("نوع سابقه آموزشی")]
        public EducationalExperienceType Type { get; set; }

        /// <summary>
        /// آی دی استاد مرتبط با سابقه تدریس
        /// </summary>
        [Required]
        public  Guid TeacherId { get; set; }

        /// <summary>
        ///آی دی عنوان تدریس شده یا درس
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان را نتخاب کنید")]
        [DisplayName("عنوان تدریس شده")]
        public  Guid TitleId { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست عنوان ها برای لیست آبشاری در ویو
        /// </summary>
        public IEnumerable<SelectListItem> Titles { get; set; } 
        #endregion
    }
}