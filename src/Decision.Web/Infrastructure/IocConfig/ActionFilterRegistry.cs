using System.Linq;
using System.Web.Http.Filters;
using StructureMap;
using StructureMap.TypeRules;

namespace Decision.Web.Infrastructure.IocConfig
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry()
        {
            Policies.SetAllProperties(p =>
            {
                p.Matching(x => x.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute))
                                &&
                                x.DeclaringType.Namespace.StartsWith(
                                    typeof(ActionFilterRegistry).Namespace.Split('.').First())
                                && !x.PropertyType.IsPrimitive && x.PropertyType != typeof(string)
                    );
            });
        }
    }
}