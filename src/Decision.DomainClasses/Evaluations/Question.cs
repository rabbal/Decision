using System.Collections.Generic;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class Question : TrackableEntity<long, User>
    {
        #region Constructor

        public Question()
        {
            Options = new HashSet<AnswerOption>();
            Interviews = new HashSet<Interview>();
            EvaluationPeriods = new HashSet<EvaluationPeriod>();
        }
        #endregion

        #region Properties

        public string Title { get; set; }

        public int Ratio { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<AnswerOption> Options { get; set; }
        public ICollection<Interview> Interviews { get; set; }
        public ICollection<EvaluationPeriod> EvaluationPeriods { get; set; }

        #endregion
    }
}