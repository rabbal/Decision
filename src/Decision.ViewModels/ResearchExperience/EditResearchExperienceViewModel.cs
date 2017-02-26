using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Applicants;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.ResearchExperience
{
    public class EditResearchExperienceViewModel:BaseRowVersion
    {
        #region Properties
        public Guid Id { get; set; }

        [Required(ErrorMessage = "لطفا عنوان پژوهش را وارد کنید")]
        [DisplayName("عنوان")]
        
        public  string Title { get; set; }

        [DisplayName("نوع پژوهش")]
        public  ResearchType ResearchType { get; set; }

        [DisplayName("ناشر")]
        public  string PublishedIn { get; set; }

        [DisplayName("سال انتشار")]
        public  DateTime PublishDate { get; set; }

        [DisplayName("توضیحات")]
        public  string Description { get; set; }
        [Required]
        public  Guid ApplicantId { get; set; }
        #endregion  
    }
}