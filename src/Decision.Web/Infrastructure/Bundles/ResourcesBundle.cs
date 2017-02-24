using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public class ResourcesBundle : ScriptBundle
    {
        public ResourcesBundle(string virtualPath)
            : base(virtualPath, null)
        {
        }
    }
}