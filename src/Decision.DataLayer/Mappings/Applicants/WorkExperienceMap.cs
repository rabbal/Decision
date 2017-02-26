using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Applicants;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class WorkExperienceMap : TrackableEntityMap<WorkExperience, long>
    {

        public WorkExperienceMap()
        {
            Property(w => w.OfficeName).HasMaxLength(1024).IsRequired();
        }
    }
}
