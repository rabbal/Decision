using System.Web.Mvc;
using Decision.Framework.MvcToolkit.ModelMetadata;
using Decision.Framework.MvcToolkit.ModelMetadata.Filters;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ModelMetadataRegistry : Registry
    {
        public ModelMetadataRegistry()
        {
            For<ModelMetadataProvider>().Use<ExtensiableModelMetadataProvider>();

            Scan(scan =>
            {
                scan.AssemblyContainingType<ReadOnlyTemplateSelectorFilter>();
                scan.AddAllTypesOf<IModelMetadataFilter>();
            });
        }
    }
}