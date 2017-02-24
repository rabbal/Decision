using System.Web.Mvc;
using Decision.Common.MvcToolkit.ModelMetadata;
using Decision.Common.MvcToolkit.ModelMetadata.Filters;
using StructureMap;

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