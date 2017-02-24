using System;
using System.Collections.Generic;

namespace Decision.Common.MvcToolkit.ModelMetadata
{
    public interface IModelMetadataFilter
    {
        void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes);
    }
}
