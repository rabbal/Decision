using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.ResearchExperience
{
    public class AddResearchExperienceViewModel
    {
        public AddResearchExperienceViewModel()
        {
            PublishDate=DateTime.Now;
        }
        #region Properties
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