using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.DomainClasses.Entities.Common;

namespace Decision.DomainClasses.Entities.Evaluations
{
    /// <summary>
    /// نشان دهنده این است که در یک ارزیابی به یک سوال با جواب متغییر چه مقداری داده شده است
    /// </summary>
    public class ArticleEvaluationQuestion : BaseEntity
    {
        #region Properties
        /// <summary>
        /// محتوای پاسخ داده شده
        /// int/string
        /// </summary>
        public  string Value { get; set; }

        #endregion

        #region NavigationProperties
        /// <summary>
        /// ارزیابی که در آن به فلان سوال جواب داده شده است
        /// </summary>
        public ArticleEvaluation ArticleEvaluation { get; set; }
        /// <summary>
        ///آی دی ارزیابی که در آن به فلان سوال جواب داده شده است
        /// </summary>
        public  Guid ArticleEvaluationId { get; set; }
        /// <summary>
        /// سوالی که به آن جواب داده شده است
        /// </summary>
        public  Question Question { get; set; }
        /// <summary>
        /// آی دی سوالی که به آن جواب داده شده است
        /// </summary>
        public  Guid QuestionId { get; set; }
        #endregion
    }
}
