using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
namespace Decision.Web.Helpers
{
    public sealed class LazySingleton
    {
        private static readonly  Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton(),
            LazyThreadSafetyMode.ExecutionAndPublication);

        private LazySingleton() { }

        public static  LazySingleton Instance => _instance.Value;

       

    }
}