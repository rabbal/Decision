using System;
using System.Collections.Generic;
using Decision.Framework.Domain.Tracking;
using Decision.DomainClasses.Identity;

namespace Decision.DomainClasses.Evaluations
{
    public class AnswerOption : TrackableEntity<long, User>
    {
        #region Properties

        public string Title { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

        #region NavigationProperties

        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        public virtual Question Question { get; set; }

        public virtual long QuestionId { get; set; }

        #endregion
    }
}