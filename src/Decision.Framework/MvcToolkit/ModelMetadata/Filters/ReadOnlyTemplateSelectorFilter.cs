using System;
using System.Collections.Generic;

namespace Decision.Framework.MvcToolkit.ModelMetadata.Filters
{
    /// <summary>
    /// The read only template selector filter.
    /// </summary>
    public class ReadOnlyTemplateSelectorFilter : IModelMetadataFilter
    {
        /// <summary>
        /// The transform metadata.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="attributes">
        /// The attributes.
        /// </param>
        public void TransformMetadata(
            System.Web.Mvc.ModelMetadata metadata, 
            IEnumerable<Attribute> attributes)
        {
            if (metadata.IsReadOnly && string.IsNullOrEmpty(metadata.DataTypeName))
            {
                metadata.DataTypeName = "ReadOnly";
            }
        }
    }
}