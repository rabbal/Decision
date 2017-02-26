using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Web.Hosting;
using Decision.Framework.Extensions;
using Decision.Framework.GuardToolkit;
using Decision.Framework.Infrastructure;

namespace Decision.Framework.Utility
{
    public static class CommonHelper
    {
        public static bool IsDevEnvironment
        {
            get
            {
                if (!HostingEnvironment.IsHosted)
                    return true;

                if (HostingEnvironment.IsDevelopmentEnvironment)
                    return true;

                if (Debugger.IsAttached)
                    return true;

                // if there's a 'SmartStore.NET.sln' in one of the parent folders,
                // then we're likely in a dev environment
                return FindSolutionRoot(HostingEnvironment.MapPath("~/")) != null;
            }
        }

        /// <summary>
        ///     Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        ///     Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        ///     Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <param name="findAppRoot">Specifies if the app root should be resolved when mapped directory does not exist</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        /// <remarks>
        ///     This method is able to resolve the web application root
        ///     even when it's called during design-time (e.g. from EF design-time tools).
        /// </remarks>
        public static string MapPath(string path, bool findAppRoot = true)
        {
            var path1 = path;
            Check.ArgumentNotNull(() => path1);

            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }
            // not hosted. For example, running in unit tests or EF tooling
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');

            var testPath = Path.Combine(baseDirectory, path);

            if (!findAppRoot) return testPath;

            // most likely we're in unit tests or design-mode (EF migration scaffolding)...
            // find solution root directory first
            var dir = FindSolutionRoot(baseDirectory);

            // concat the web root
            if (dir == null) return testPath;
            baseDirectory = Path.Combine(dir.FullName, "NTierMvcFamework.Web");
            testPath = Path.Combine(baseDirectory, path);

            return testPath;
        }

        private static DirectoryInfo FindSolutionRoot(string currentDir)
        {
            var dir = Directory.GetParent(currentDir);
            while (true)
            {
                if (dir == null || IsSolutionRoot(dir))
                    break;

                dir = dir.Parent;
            }

            return dir;
        }

        private static bool IsSolutionRoot(FileSystemInfo dir)
        {
            return File.Exists(Path.Combine(dir.FullName, "NTierMvcFramework.sln"));
        }


        public static TypeConverter GetTypeConverter(Type type)
        {
            return ConversionExtensions.GetTypeConverter(type);
        }

        /// <summary>
        ///     Gets a setting from the application's <c>web.config</c> <c>appSettings</c> node
        /// </summary>
        /// <typeparam name="T">The type to convert the setting value to</typeparam>
        /// <param name="key">The key of the setting</param>
        /// <param name="defValue">The default value to return if the setting does not exist</param>
        /// <returns>The casted setting value</returns>
        public static T GetAppSetting<T>(string key, T defValue = default(T))
        {
            Check.ArgumentNotEmpty(() => key);

            var setting = ConfigurationManager.AppSettings[key];

            return setting == null ? defValue : setting.Convert<T>();
        }
    }
}