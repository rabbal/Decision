using System.Collections;
using System.Web.Routing;

namespace Decision.Framework.MvcToolkit.Extensions
{
    public static class MiscExtensions
    {
        public static RouteValueDictionary ToRouteValueDictionaryWithCollection(this RouteValueDictionary routeValues)
        {
            var newRouteValues = new RouteValueDictionary();

            foreach (var key in routeValues.Keys)
            {
                var value = routeValues[key];

                var vals = value as IEnumerable;
                if (vals != null && !(value is string))
                {
                    var index = 0;
                    foreach (var val in vals)
                    {
                        if (val is string || val.GetType().IsPrimitive)
                        {
                            newRouteValues.Add($"{key}[{index}]", val);
                        }
                        else
                        {
                            var properties = val.GetType().GetProperties();
                            foreach (var propInfo in properties)
                            {
                                newRouteValues.Add(
                                    $"{key}[{index}].{propInfo.Name}",
                                    propInfo.GetValue(val));
                            }
                        }
                        index++;
                    }
                }
                else
                {
                    newRouteValues.Add(key, value);
                }
            }

            return newRouteValues;
        }

    }
}