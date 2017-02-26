using Decision.Framework.Domain.Entities;

namespace Decision.DomainClasses.Evaluations
{
    public class InterviewAnswer : Entity
    {
        #region Navigation Properties

        public Question Question { get; set; }
        public long QuestionId { get; set; }
        public AnswerOption AnswerOption { get; set; }
        public long AnswerOptionId { get; set; }
        public Interview Interview { get; set; }
        public long InterviewId { get; set; }
        #endregion
    }
}
