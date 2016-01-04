using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده یک پاسخ برای سوال
    /// </summary>
    public class AnswerOption 
    {
        #region Ctor

        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AnswerOption()
        {
            Id = SequentialGuidGenerator.NewSequentialGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// آی دی
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// نام گزینه پاسخ
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// وزن ارزشی گزینه
        /// </summary>
        public virtual byte Weight { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست ارزیابی هایی که این گزینه رو انتخاب کرده اند
        /// </summary>
        public virtual ICollection<ArticleEvaluation> ArticleEvaluations { get; set; }

        /// <summary>
        /// سوالی که این گزینه جز پاسخ های آن است
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        ///  آی دی سوالی که این گزینه جز پاسخ های آن است
        /// </summary>
        public virtual Guid QuestionId { get; set; }
        #endregion
    }
}
