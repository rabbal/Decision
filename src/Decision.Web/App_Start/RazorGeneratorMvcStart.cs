using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Decision.Framework.MvcToolkit.ViewEngines;
using Decision.Web;
using RazorGenerator.Mvc;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(RazorGeneratorMvcStart), "Start")]

namespace Decision.Web
{
    public static class RazorGeneratorMvcStart
    {
        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

#if DEBUG
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());
#else
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly) {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine);

            // StartPage lookups are done by WebPages.
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
#endif
        }
    }
}
