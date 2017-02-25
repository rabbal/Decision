using System;
using System.Collections.Generic;

namespace Decision.Framework.MvcToolkit.Helpers
{
    public static class AngularTemplateHelpers
    {
        #region Fields (1)
        private static Dictionary<Type, string> TemplateMap = new Dictionary<Type, string>
        {
            { typeof(byte), "Number"},
            { typeof(sbyte), "Number"},
            { typeof(int), "Number"},
            { typeof(uint), "Number"},
            { typeof(long), "Number"},
            { typeof(ulong), "Number"},
            { typeof(bool), "Boolean"},
            { typeof(decimal), "Decimal"}
        };
        #endregion

        #region Methods (1)
        public static string GetTemplateForProperty(System.Web.Mvc.ModelMetadata propertyMetadata)
        {
            var templateName = (propertyMetadata.TemplateHint ?? propertyMetadata.DataTypeName) ??
                               (TemplateMap.ContainsKey(propertyMetadata.ModelType) ?
                TemplateMap[propertyMetadata.ModelType] :
                propertyMetadata.ModelType.Name);

            return $"Angular/{templateName}";
        }
        #endregion
    }
}