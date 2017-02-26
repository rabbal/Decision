using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Evaluations;

namespace Decision.DataLayer.Mappings.Evaluations
{
    public class QuestionMap : TrackableEntityMap<Question, long>
    {
        public QuestionMap()
        {
            Property(q => q.Title).IsMaxLength().IsRequired();

            HasMany(q => q.Options)
                .WithRequired(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .WillCascadeOnDelete(true);

        }
    }
}
