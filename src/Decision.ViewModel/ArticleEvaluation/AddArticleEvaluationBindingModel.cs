using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModel.ArticleEvaluation
{
    public class AddArticleEvaluationBindingModel
    {
        public IEnumerable<NumericQuestion> NumericQuestions { get; set; }
        public IEnumerable<StringQuestion> StringQuestions { get; set; }
        public IEnumerable<RadioButtonListQuestion> RadioButtonListQuestions { get; set; }
        public IEnumerable<CheckBoxListQuestion> CheckBoxListQuestions { get; set; }
        /// <summary>
        ///  نظریه کلی برای مقاله
        /// </summary>
        [AllowHtml]
        public string Content { get; set; }
        /// <summary>
        /// خلاصه ارزیابی از مقاله 
        /// </summary>
        [AllowHtml]
        public string Brief { get; set; }
        /// <summary>
        /// نقاط ضعف مقاله
        /// </summary>
        [AllowHtml]
        public string Foible { get; set; }
        /// <summary>
        /// نقطه قوت مقاله
        /// </summary>
        [AllowHtml]
        public string StrongPoint { get; set; }
        /// <summary>
        /// آی دی ارزیاب مقاله صادر شده
        /// </summary>
        [Required(ErrorMessage = "لطفا ارزیاب را انتخاب کنید")]
        public Guid EvaluatorId { get; set; }
        /// <summary>
        /// آی دی مقاله 
        /// </summary>
        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public Guid ApplicantId { get; set; }
    }
}
