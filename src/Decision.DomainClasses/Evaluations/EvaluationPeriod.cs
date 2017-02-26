using System;
using System.Collections.Generic;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class EvaluationPeriod : TrackableEntity
    {
        #region Properties
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        #endregion

        #region Navigation Properties
        public ICollection<Question> Questions { get; set; }
        #endregion

    }
}
