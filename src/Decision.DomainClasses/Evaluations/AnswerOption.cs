using System.Collections.Generic;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class AnswerOption : TrackableEntity<long, User>
    {
        #region Properties

        public string Title { get; set; }

        public int Ratio { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        public virtual Question Question { get; set; }

        public virtual long QuestionId { get; set; }

        #endregion
    }
}