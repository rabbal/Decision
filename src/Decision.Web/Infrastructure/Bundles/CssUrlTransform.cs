using System.IO;
using System.Text;
using System.Web.Optimization;

namespace Decision.Web.Infrastructure.Bundles
{
    /// <summary>
    ///     Css transformation for bundling, that will replace URL relative paths with updated relative paths for the new
    ///     bundle location.
    /// </summary>
    public class CssUrlTransform : IBundleTransform
    {
        /// <summary>
        ///     Processes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="response">The response.</param>
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = string.Empty;

            var builder = new StringBuilder();

            builder.Append(response.Content);

            // regex that looks for all "URL(*)" statements in a css file
            foreach (var bundleFile in response.Files)
            {
                // determine the full file path and read all its contents
                // we cannot use response.Content since that contains content from ALL consolidated files.
                var cssFile = context.HttpContext.Server.MapPath(bundleFile.IncludedVirtualPath);
                var cssContent = File.ReadAllText(cssFile);

                // transform the content by reutilizing the built in item level transform for each file.
                var transform = new CssRewriteUrlTransform();
                builder.Append(transform.Process(bundleFile.IncludedVirtualPath, cssContent));
            }
            response.Content = builder.ToString();
        }
    }
}