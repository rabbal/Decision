using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
namespace Decision.Web.Helpers
{
    public static class LazySingleton<T>
        where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T(),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static T Instance => _instance.Value;
    }
}