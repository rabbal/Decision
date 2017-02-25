using System.Collections.Generic;
using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class Question : TrackableEntity<long, User>
    {
        #region Properties

        public string Title { get; set; }

        public bool IsMultiSelect { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        #endregion

        #region NavigationProperties

        public ICollection<AnswerOption> AnswerOptions { get; set; }

        public ICollection<EntireEvaluation> EntireEvaluations { get; set; }

        #endregion
    }
}