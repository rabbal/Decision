using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Decision.Framework.Infrastructure
{
    /// <summary>
    ///     A class that finds types needed by SmartStore by looping assemblies in the
    ///     currently executing AppDomain. Only assemblies whose names matches
    ///     certain patterns are investigated and an optional list of assemblies
    ///     referenced by <see cref="CustomAssemblyNames" /> are always investigated.
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        /// <summary>Iterates all assemblies in the AppDomain and if it's name matches the configured patterns add it to our list.</summary>
        /// <param name="addedAssemblyNames"></param>
        /// <param name="assemblies"></param>
        private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            if (addedAssemblyNames == null) throw new ArgumentNullException(nameof(addedAssemblyNames));
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Matches(assembly.FullName))
                {
                    if (!addedAssemblyNames.Contains(assembly.FullName))
                    {
                        assemblies.Add(assembly);
                        addedAssemblyNames.Add(assembly.FullName);
                    }
                }
            }
        }

        protected virtual void AddCustomAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies)
        {
            foreach (var assemblyName in CustomAssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);
                if (!addedAssemblyNames.Contains(assembly.FullName))
                {
                    assemblies.Add(assembly);
                    addedAssemblyNames.Add(assembly.FullName);
                }
            }
        }

        /// <summary>Check if a dll is one of the shipped dlls that we know don't need to be investigated.</summary>
        /// <param name="assemblyFullName">The name of the assembly to check.</param>
        /// <returns>True if the assembly should be loaded into SmartStore.</returns>
        public virtual bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, AssemblySkipLoadingPattern)
                   && Matches(assemblyFullName, AssemblyRestrictToLoadingPattern);
        }

        /// <summary>Check if a dll is one of the shipped dlls that we know don't need to be investigated.</summary>
        /// <param name="assemblyFullName">The assembly name to match.</param>
        /// <param name="pattern">The regular expression pattern to match against the assembly name.</param>
        /// <returns>True if the pattern matches the assembly name.</returns>
        protected virtual bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>Makes sure matching assemblies in the supplied folder are loaded in the app domain.</summary>
        /// <param name="directoryPath">The physical path to a directory containing dlls to load in the app domain.</param>
        protected virtual void LoadMatchingAssemblies(string directoryPath)
        {
            var loadedAssemblyNames = GetAssemblies().Select(a => a.FullName).ToList();

            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            foreach (var dllPath in Directory.GetFiles(directoryPath, "*.dll"))
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllPath);
                    if (Matches(an.FullName) && !loadedAssemblyNames.Contains(an.FullName))
                    {
                        App.Load(an);
                    }

                    //old loading stuff
                    //Assembly a = Assembly.ReflectionOnlyLoadFrom(dllPath);
                    //if (Matches(a.FullName) && !loadedAssemblyNames.Contains(a.FullName))
                    //{
                    //    App.Load(a.FullName);
                    //}
                }
                catch (BadImageFormatException ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }
        }

        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        #region Private Fields

        private readonly bool _ignoreReflectionErrors = true;

        #endregion

        #region Properties

        /// <summary>The app domain to look for types in.</summary>
        public virtual AppDomain App => AppDomain.CurrentDomain;

        /// <summary>
        ///     Gets or sets wether SmartStore should iterate assemblies in the app domain when loading SmartStore types.
        ///     Loading patterns are applied when loading these assemblies.
        /// </summary>
        public bool LoadAppDomainAssemblies { get; set; } = true;

        /// <summary>Gets or sets assemblies loaded at startup in addition to those loaded in the AppDomain.</summary>
        public IList<string> CustomAssemblyNames { get; set; } = new List<string>();

        /// <summary>Gets the pattern for dlls that we know don't need to be investigated.</summary>
        public string AssemblySkipLoadingPattern { get; set; } =
            @"^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^WebDev|^Nuget|^Castle|^Iesi|^log4net|^Autofac|^AutoMapper|^EntityFramework|^EPPlus|^Fasterflect|^nunit|^TestDriven|^MbUnit|^Rhino|^QuickGraph|^TestFu|^Telerik|^Antlr3|^Recaptcha|^FluentValidation|^ImageResizer|^itextsharp|^MiniProfiler|^Newtonsoft|^Pandora|^WebGrease|^Noesis|^DotNetOpenAuth|^Facebook|^LinqToTwitter|^PerceptiveMCAPI|^CookComputing|^GCheckout|^Mono\.Math|^Org\.Mentalis|^App_Web|^BundleTransformer|^ClearScript|^JavaScriptEngineSwitcher|^MsieJavaScriptEngine|^Glimpse|^Ionic|^App_GlobalResources|^AjaxMin|^MaxMind|^NReco|^OffAmazonPayments|^UAParser"
            ;

        /// <summary>
        ///     Gets or sets the pattern for dll that will be investigated. For ease of use this defaults to match all but to
        ///     increase performance you might want to configure a pattern that includes assemblies and your own.
        /// </summary>
        /// <remarks>
        ///     If you change this so that SmartStore assemblies arn't investigated (e.g. by not including something like
        ///     "^SmartStore|..." you may break core functionality.
        /// </remarks>
        public string AssemblyRestrictToLoadingPattern { get; set; } = ".*";

        #endregion

        #region ITypeFinder

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies,
            bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();

            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch (Exception)
                    {
                        //Entity Framework 6 doesn't allow getting types (throws an exception)
                        if (!_ignoreReflectionErrors)
                        {
                            throw;
                        }
                    }
                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (assignTypeFrom.IsAssignableFrom(t) ||
                                (assignTypeFrom.IsGenericTypeDefinition &&
                                 DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            {
                                if (!t.IsInterface)
                                {
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.IsClass && !t.IsAbstract)
                                        {
                                            result.Add(t);
                                        }
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                Debug.WriteLine(fail.Message, fail);

                throw fail;
            }
            return result;
        }


        /// <summary>Gets the assemblies related to the current implementation.</summary>
        /// <param name="ignoreInactivePlugins">Indicates whether uninstalled plugin's assemblies should be filtered out</param>
        /// <returns>A list of assemblies that should be loaded by the SmartStore factory.</returns>
        public virtual IList<Assembly> GetAssemblies(bool ignoreInactivePlugins = false)
        {
            var addedAssemblyNames = new List<string>();
            var assemblies = new List<Assembly>();

            if (LoadAppDomainAssemblies)
            {
                AddAssembliesInAppDomain(addedAssemblyNames, assemblies);
            }
            AddCustomAssemblies(addedAssemblyNames, assemblies);

            return assemblies;
        }

        #endregion
    }
}