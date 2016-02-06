using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Decision.ViewModel.ArticleEvaluation
{
    /// <summary>
    /// ویومدل درج ارزیابی از مقاله متقاضی
    /// </summary>
    public class AddArticleEvaluationViewModel
    {
        #region Properties
        public Guid ArticleId { get; set; }
        public Guid ApplicantId { get; set; }
        /// <summary>
        ///  نظریه کلی برای مقاله
        /// </summary>
        [DisplayName("متن ارزیابی")]
        public  string Content { get; set; }
        /// <summary>
        /// خلاصه ارزیابی از مقاله 
        /// </summary>
        [DisplayName("خلاصه ارزیابی")]
        public  string Brief { get; set; }

        /// <summary>
        /// نقاط ضعف مقاله
        /// </summary>
        [DisplayName("نقاط ضعف")]
        public  string Foible { get; set; }

        /// <summary>
        /// نقطه قوت مقاله
        /// </summary>
        [DisplayName("نقاط قوت")]
        public  string StrongPoint { get; set; }
        /// <summary>
        /// آی دی ارزیاب مقاله صادر شده
        /// </summary>
        [Required(ErrorMessage = "لطفا ارزیاب را انتخاب کنید")]
        [DisplayName("ارزیاب")]
        public  Guid EvaluatorId { get; set; }
        
        public IEnumerable<DomainClasses.Entities.Evaluations.Question> Questions { get; set; }
        #endregion

        #region SelectListItems
        /// <summary>
        /// لیست ارزیاب ها برای لیست آبشاری در ویو
        /// </summary>
        public  IEnumerable<SelectListItem> Evaluators { get; set; }
        #endregion
    }
}