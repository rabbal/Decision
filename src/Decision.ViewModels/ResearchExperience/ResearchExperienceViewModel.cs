using System;
using System.ComponentModel;
using Decision.DomainClasses.Applicants;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.ResearchExperience
{
    public class ResearchExperienceViewModel:BaseViewModel
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
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
        #endregion  
    }
}