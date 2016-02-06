using System.Collections.Generic;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده سوال 
    /// </summary>
    public class Question : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان سوال
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// امکان انتخاب چند تایی
        /// </summary>
        public virtual bool IsMultiSelect { get; set; }
        /// <summary>
        /// وزن ارزشی سوال
        /// </summary>
        public virtual int Weight { get; set; }
        /// <summary>
        /// توضیحات اضافی
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ترتیب نمایش
        /// </summary>
        public int DisplayOrder { get; set; }
        #endregion

        #region NavigationProperties
        /// <summary>
        /// پاسخ های چند گرینه ای سوال
        /// </summary>
        public virtual ICollection<AnswerOption> AnswerOptions { get; set; }
        /// <summary>
        /// لیست ارزیابی هایی که این سوال را جواب داده اند
        /// </summary>
        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }
        #endregion
    }
}
