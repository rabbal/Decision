using Decision.DomainClasses.Identity;
using Decision.Framework.Domain.Entities.Tracking;

namespace Decision.DomainClasses.Evaluations
{
    public class QuestionCategory : TrackableEntity<long, User>
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
    }
}
