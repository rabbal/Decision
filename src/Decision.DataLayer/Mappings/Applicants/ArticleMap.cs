using System.Data.Entity.ModelConfiguration;
using Decision.DomainClasses.Applicants;

namespace Decision.DataLayer.Mappings.Applicants
{
    public class ArticleMap : TrackableEntityMap<Article, long>
    {
        public ArticleMap()
        {
            Property(j => j.Brief).IsMaxLength().IsOptional();
            Property(j => j.RowVersion).IsRowVersion();
        }
    }
}
