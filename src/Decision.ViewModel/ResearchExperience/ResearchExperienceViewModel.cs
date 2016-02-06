using System;
using System.ComponentModel;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ResearchExperience
{
    /// <summary>
    /// ویومدل نمایش سابقه پژوهشی
    /// </summary>
    public class ResearchExperienceViewModel:BaseViewModel
    {
        #region Properties
        /// <summary>
        /// آی دی 
        /// </summary>
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        /// <summary>
        /// عنوای پژوهش
        /// </summary>
        [DisplayName("عنوان")]
        public  string Title { get; set; }

        /// <summary>
        /// نوع پژوهش
        /// </summary>
        [DisplayName("نوع پژوهش")]
        public  PresenterType ResearchType { get; set; }

        /// <summary>
        ///  چاپ شده توسط  
        /// </summary>
        [DisplayName("ناشر")]
        public  string PublishedIn { get; set; }

        /// <summary>
        ///  سال انتشار  
        /// </summary>
        [DisplayName("سال انتشار")]
        public  DateTime PublishDate { get; set; }

        /// <summary>
        /// توضیحات اضافی
        /// </summary>
        [DisplayName("توضیحات")]
        public  string Description { get; set; }
        #endregion  
    }
}