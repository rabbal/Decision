using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using NTierMvcFramework.Common.IO.VirtualPath;

namespace NTierMvcFramework.Common.IO.WebSite
{
    public class WebSiteFolder : IWebSiteFolder
    {
        private readonly IVirtualPathProvider _virtualPathProvider;

        public WebSiteFolder(IVirtualPathProvider virtualPathProvider)
        {
            _virtualPathProvider = virtualPathProvider;
        }

        public IEnumerable<string> ListDirectories(string virtualPath)
        {
            if (!_virtualPathProvider.DirectoryExists(virtualPath))
            {
                return Enumerable.Empty<string>();
            }

            return _virtualPathProvider.ListDirectories(virtualPath);
        }

        public IEnumerable<string> ListFiles(string virtualPath, bool recursive)
        {
            return !recursive
                ? _virtualPathProvider.ListFiles(virtualPath)
                : _virtualPathProvider.ListFiles(virtualPath).Concat(ListFiles(ListDirectories(virtualPath)));
        }

        public bool FileExists(string virtualPath)
        {
            return _virtualPathProvider.FileExists(virtualPath);
        }


        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public string ReadFile(string virtualPath)
        {
            if (!_virtualPathProvider.FileExists(virtualPath))
            {
                return null;
            }

            using (var stream = _virtualPathProvider.OpenFile(Normalize(virtualPath)))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public void CopyFileTo(string virtualPath, Stream destination)
        {
            using (var stream = _virtualPathProvider.OpenFile(Normalize(virtualPath)))
            {
                stream.CopyTo(destination);
            }
        }

        private IEnumerable<string> ListFiles(IEnumerable<string> directories)
        {
            return directories.SelectMany(d => ListFiles(d, true));
        }

        private string Normalize(string virtualPath)
        {
            return _virtualPathProvider.Normalize(virtualPath);
        }
    }
}