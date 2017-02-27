using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Evaluations
{
    public class QuestionCategory : Entity
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
    }
}
