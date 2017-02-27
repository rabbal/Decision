using System;
using System.ComponentModel;

namespace Decision.ViewModels.ApplicantEvaluation.Interview
{
    public class InterviewViewModel :BaseViewModel
    {
        #region Properties
        public  Guid Id { get; set; }
        public Guid ApplicantId { get; set; }

        [DisplayName("تاریخ مصاحبه")]
        public  DateTime InterviewDate { get; set; }

        [DisplayName("متن مصاحبه")]
        public  string Body { get; set; }

        [DisplayName("خلاصه")]
        public  string Brief { get; set; }

        [DisplayName("مصاحبه کننده")]
        public  string InterviewerName { get; set; }
        #endregion
    }
}