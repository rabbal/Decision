using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dispatcher;
using NTierMvcFramework.Common.WebAPIToolkit.Controller;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    /// <summary>
    ///     for self host
    ///     todo:config.Services.Replace(typeof(IAssembliesResolver), new CustomAssembliesResolver());
    /// </summary>
    public class CustomAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var baseAssemblies = base.GetAssemblies().ToList();
            var assemblies = new List<Assembly>(baseAssemblies) {typeof(BaseApiController).Assembly};

            //todo: unreferenced dll
            //var assemblies = new List<Assembly>(baseAssemblies);
            //var unreferencedAssembly = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.
            //GetExecutingAssembly().Location), "UnreferencedExternalLibrary.dll"));
            //assemblies.Add(unreferencedAssembly);
            //return assemblies;

            return assemblies.Distinct().ToList();
        }
    }
}