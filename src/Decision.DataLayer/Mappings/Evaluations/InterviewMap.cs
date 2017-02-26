using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Evaluations;

namespace Decision.DataLayer.Mappings.Evaluations
{
    public class InterviewMap : TrackableEntityMap<Interview, long>
    {
        public InterviewMap()
        {
            Property(i => i.Content).IsMaxLength().IsRequired();
        }
    }
}
