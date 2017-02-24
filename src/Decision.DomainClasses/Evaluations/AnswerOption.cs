using System;
using System.Collections.Generic;

namespace Decision.DomainClasses.Evaluations
{
    public class AnswerOption : BaseEntity
    {
        #region Properties

        public virtual string Title { get; set; }

        public virtual int Weight { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

        #region NavigationProperties

        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        public virtual Question Question { get; set; }

        public virtual Guid QuestionId { get; set; }

        #endregion
    }
}