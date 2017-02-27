using System;
using System.ComponentModel;
using Decision.DomainClasses.Applicants;

namespace Decision.ViewModels.GeneralBasicData.WorkExperience
{
    public class WorkExperienceViewModel:BaseViewModel
    {
        #region Properties
        public  Guid Id { get; set; }

        public Guid ApplicantId { get; set; }
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

        [DisplayName("اداره محل خدمت")]
        public  string OfficeName { get; set; }
        
        [DisplayName("شهر")]
        public  string City { get; set; }
        [DisplayName("استان")]
        public string State { get; set; }

        [DisplayName("پست سازمانی")]
        public  string TitleName { get; set; }
        #endregion
    }
}