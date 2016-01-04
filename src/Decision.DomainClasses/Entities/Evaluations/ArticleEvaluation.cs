using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Decision.DomainClasses.Entities.Common;
using Decision.DomainClasses.Entities.TeacherInfo;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده ارزیابی انجام شده از مقاله استاد
    /// </summary>
    public class ArticleEvaluation : BaseEntity
    {

        #region Ctor
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public ArticleEvaluation()
        {
            ArticleEvaluationQuestions = new List<ArticleEvaluationQuestion>();
            AnswerOptions=new List<AnswerOption>();
            Questions=new List<Question>();
            EvaluationDate = DateTime.Now;
        }
        #endregion

        #region Properties
        /// <summary>
        ///  نظریه کلی برای مقاله
        /// </summary>
        public  string Content { get; set; }
        /// <summary>
        /// تاریخ ارزیابی
        /// </summary>
        public  DateTime EvaluationDate { get; set; }
        /// <summary>
        /// خلاصه ارزیابی از مقاله 
        /// </summary>
        public  string Brief { get; set; }
        /// <summary>
        /// نقاط ضعف مقاله
        /// </summary>
        public  string Foible { get; set; }
        /// <summary>
        /// نقطه قوت مقاله
        /// </summary>
        public  string StrongPoint { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// مقاله مورد نظر که ارزیابی بر روی آن انجام شده
        /// </summary>
        public  Article Article { get; set; }
        /// <summary>
        /// آی دی مقاله مورد نظر که ارزیابی بر روی آن انجام شده
        /// </summary>
        public  Guid ArticleId { get; set; }
        /// <summary>
        /// ارزیاب مقاله صادر شده
        /// </summary>
        public  Appraiser Evaluator { get; set; }
        /// <summary>
        /// آی دی ارزیاب مقاله صادر شده
        /// </summary>
        public  Guid EvaluatorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  ICollection<ArticleEvaluationQuestion> ArticleEvaluationQuestions { get; set; }
        /// <summary>
        /// لیست گزینه های پاسخ که در این ارزیابی انتخاب شده اند
        /// </summary>
        public  ICollection<AnswerOption> AnswerOptions  { get; set; }
        /// <summary>
        /// لیست سوالاتی که در این ارزیابی به آنها پاسخ داده شده است
        /// </summary>
        public  ICollection<Question> Questions  { get; set; }

        #endregion
    }
}
