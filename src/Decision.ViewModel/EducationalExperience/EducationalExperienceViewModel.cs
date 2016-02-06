using System;
using System.ComponentModel;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.EducationalExperience
{
    /// <summary>
    /// ویومدل نمایش سابقه آموزشی
    /// </summary>
    public class EducationalExperienceViewModel :BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی سابقه آموزشی
        /// </summary>
        public  Guid Id { get; set; }
        public Guid ApplicantId { get; set; }

        /// <summary>
        /// فیلد مشترک برای موارد زیر
        /// <remarks>نام سازمان مربوطه/توضیحات/نام مرکز علمی</remarks>
        /// </summary>
        public  string Description { get; set; }

        ///<summary>
        /// سال آغاز  
        /// </summary>
        [DisplayName("سال آغاز")]
        public int BeginYear { get; set; }

        /// <summary>
        /// سال پایان 
        /// </summary>
        [DisplayName("سال پایان")]
        public int EndYear { get; set; }


        /// <summary>
        ///عنوان تدریس شده یا درس
        /// </summary>
        [DisplayName("عنوان")]
        public  string TitleName { get; set; }
        #endregion
    }
}