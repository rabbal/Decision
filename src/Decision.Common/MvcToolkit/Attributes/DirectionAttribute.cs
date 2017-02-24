using System;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Attributes
{
    public sealed class DirectionAttribute : Attribute, IMetadataAware
    {
        public string Direction { get; set; } = "ltr";
        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues["dir"] = Direction;
        }
    }


}