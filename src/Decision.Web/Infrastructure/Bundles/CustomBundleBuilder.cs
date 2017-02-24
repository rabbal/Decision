using System;
using System.Collections.Generic;
using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    public class CustomBundleBuilder : IBundleBuilder
    {
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            throw new NotImplementedException();
        }
    }
}