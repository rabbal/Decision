using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Decision.Framework.EntityFrameworkToolkit.SoftDelete
{
    public static  class DbModelBuilderExtensions
    {
        public static void AddSoftDeleteConvention(this DbModelBuilder modelBuilder)
        {
            var conv = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                       "SoftDeleteColumnName",
                       (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(conv);
        }
    }
}
