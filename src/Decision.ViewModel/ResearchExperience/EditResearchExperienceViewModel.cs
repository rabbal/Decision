using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Entities.ApplicantInfo;
using Decision.ViewModel.Common;

namespace Decision.ViewModel.ResearchExperience
{
    /// <summary>
    /// ویو مدل ویرایش سابقه پژوهشی
    /// </summary>
    public class EditResearchExperienceViewModel:BaseRowVersion
    {
        #region Properties
        /// <summary>
        /// آی دی سابقه پژوهشی
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// عنوای پژوهش
        /// </summary>
        [Required(ErrorMessage = "لطفا عنوان پژوهش را وارد کنید")]
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
        [Required]
        public  Guid ApplicantId { get; set; }
        #endregion  
    }
}