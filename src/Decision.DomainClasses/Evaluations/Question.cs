using System.Collections.Generic;
using Decision.DomainClasses.Common;

namespace Decision.DomainClasses.Evaluations
{

    public class Question : BaseEntity
    {
        #region Properties

        public virtual string Title { get; set; }

        public virtual bool IsMultiSelect { get; set; }

        public virtual int Weight { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }
        #endregion

        #region NavigationProperties

        public virtual ICollection<AnswerOption> AnswerOptions { get; set; }

        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }
        #endregion
    }
}
