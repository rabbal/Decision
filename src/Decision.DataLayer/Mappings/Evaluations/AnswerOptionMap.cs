using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Evaluations;

namespace Decision.DataLayer.Mappings.Evaluations
{

    public class AnswerOptionConfig : TrackableEntityMap<AnswerOption, long>
    {
        public AnswerOptionConfig()
        {
            Property(a => a.Title).HasMaxLength(1024).IsRequired();
        }
    }
}
