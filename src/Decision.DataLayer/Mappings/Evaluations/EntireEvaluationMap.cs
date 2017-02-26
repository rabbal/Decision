using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Evaluations;

namespace Decision.DataLayer.Mappings.Evaluations
{
    public class EntireEvaluationMap : TrackableEntityMap<EntireEvaluation, long>
    {
        public EntireEvaluationMap()
        {
            Property(e => e.Content).IsMaxLength().IsRequired();

            Property(e => e.Foible).IsMaxLength().IsRequired();
            Property(e => e.StrongPoint).IsMaxLength().IsRequired();
        }
    }
}
