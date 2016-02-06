using System;
using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;
using Decision.Utility;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده یک پاسخ برای سوال
    /// </summary>
    public class AnswerOption : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان گزینه پاسخ
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// وزن ارزشی گزینه
        /// </summary>
        public virtual int Weight { get; set; }
        /// <summary>
        /// توضیحاتی در مورد گزینه
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ترتیب نمایش
        /// </summary>
        public int DisplayOrder { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// لیست ارزیابی هایی که این گزینه رو انتخاب کرده اند
        /// </summary>
        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }
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
