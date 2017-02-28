using System;
using System.Threading;

namespace Decision.Framework.Misc
{
    public static class LazySingleton<T>
        where T : class, new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T(),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static T Instance => _instance.Value;
    }
}