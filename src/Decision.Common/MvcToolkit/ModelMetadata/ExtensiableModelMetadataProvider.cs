using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.ModelMetadata
{
    public class ExtensiableModelMetadataProvider :
        DataAnnotationsModelMetadataProvider
    {
        private readonly IModelMetadataFilter[] _metadataFilters;
        public ExtensiableModelMetadataProvider(
            IModelMetadataFilter[] metadataFilters)
        {
            _metadataFilters = metadataFilters;
        }
        protected override System.Web.Mvc.ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes, 
            Type containerType, 
            Func<object> modelAccessor, 
            Type modelType, 
            string propertyName)
        {
          var metadata= base.CreateMetadata(
              attributes, 
              containerType, 
              modelAccessor, 
              modelType, 
              propertyName);

            foreach (var filter in _metadataFilters)
            {
                filter.TransformMetadata(metadata,attributes);
            }

            return metadata;
        }
    }
}
