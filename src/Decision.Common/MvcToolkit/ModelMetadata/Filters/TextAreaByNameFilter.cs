using System;
using System.Collections.Generic;

namespace NTierMvcFramework.Common.MvcToolkit.ModelMetadata.Filters
{
    public class TextAreaByNameFilter : IModelMetadataFilter
    {
        private static readonly HashSet<string> TextAreaFieldNames =
            new HashSet<string>
        {
           "Body",
           "Comments"
        };
        public void TransformMetadata(
           System.Web.Mvc.ModelMetadata metadata,
           IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName)&&
                string.IsNullOrEmpty(metadata.DataTypeName) &&
                TextAreaFieldNames.Contains(metadata.PropertyName.ToLowerInvariant()))
            {
                metadata.DataTypeName = "MultilineText";
            }
        }
    }
}