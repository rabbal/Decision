using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Applicants;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class EducationalExperienceMap : TrackableEntityMap<EducationalExperience, long>
    {
        public EducationalExperienceMap()
        {
        }
    }
}
