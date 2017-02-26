using System;
using System.ComponentModel;
using Decision.ViewModels.Common;

namespace Decision.ViewModels.EntireEvaluation
{
    public class EntireEvaluationViewModel :BaseViewModel
    {
        #region Properties
        public  Guid Id { get; set; }
        public Guid ApplicantId { get; set; }

        [DisplayName("متن ارزیابی")]
        public  string Content { get; set; }

        [DisplayName("تاریخ")]
        public  DateTime EvaluationDate { get; set; }

        [DisplayName("خلاصه ارزیابی")]
        public  string Brief { get; set; }

        [DisplayName("نقاط ضعف")]
        public  string Foible { get; set; }

        [DisplayName("نقاط قوت")]
        public  string StrongPoint { get; set; }

        [DisplayName("ارزیاب")]
        public  string EvaluatorName { get; set; }
        #endregion
    }
}