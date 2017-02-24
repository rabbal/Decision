using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Decision.Common.IO.VirtualPath
{
    public class DefaultVirtualPathProvider : IVirtualPathProvider
    {
        public virtual string GetDirectoryName(string virtualPath)
        {
            return Path.GetDirectoryName(virtualPath).Replace(Path.DirectorySeparatorChar, '/');
        }

        public virtual IEnumerable<string> ListFiles(string path)
        {
            return HostingEnvironment
                .VirtualPathProvider
                .GetDirectory(path)
                .Files
                .OfType<VirtualFile>()
                .Select(f => VirtualPathUtility.ToAppRelative(f.VirtualPath));
        }

        public virtual IEnumerable<string> ListDirectories(string path)
        {
            return HostingEnvironment
                .VirtualPathProvider
                .GetDirectory(path)
                .Directories
                .OfType<VirtualDirectory>()
                .Select(d => VirtualPathUtility.ToAppRelative(d.VirtualPath));
        }

        public virtual string Combine(params string[] paths)
        {
            return Path.Combine(paths).Replace(Path.DirectorySeparatorChar, '/');
        }

        public virtual string ToAppRelative(string virtualPath)
        {
            if (IsMalformedVirtualPath(virtualPath))
                return null;

            try
            {
                var result = VirtualPathUtility.ToAppRelative(virtualPath);


                if (result.StartsWith("~/")) return result;
                // _logger.Information("Path '{0}' cannot be made app relative: Path returned ('{1}') is not app relative.".FormatCurrent(virtualPath, result));
                return null;
            }
            catch (Exception e)
            {
                // The initial path might have been invalid (e.g. path indicates a path outside the application root)
                //_logger.Information("Path '{0}' cannot be made app relative".FormatCurrent(virtualPath), e);
                return null;
            }
        }

        public virtual Stream OpenFile(string virtualPath)
        {
            return HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open();
        }

        public virtual StreamWriter CreateText(string virtualPath)
        {
            return File.CreateText(MapPath(virtualPath));
        }

        public virtual Stream CreateFile(string virtualPath)
        {
            return File.Create(MapPath(virtualPath));
        }

        public virtual DateTime GetFileLastWriteTimeUtc(string virtualPath)
        {
            return File.GetLastWriteTimeUtc(MapPath(virtualPath));
        }

        public string GetFileHash(string virtualPath)
        {
            return GetFileHash(virtualPath, new[] {virtualPath});
        }

        public string GetFileHash(string virtualPath, IEnumerable<string> dependencies)
        {
            return HostingEnvironment.VirtualPathProvider.GetFileHash(virtualPath, dependencies);
        }

        public virtual void DeleteFile(string virtualPath)
        {
            File.Delete(MapPath(virtualPath));
        }

        public virtual string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }

        public virtual string Normalize(string virtualPath)
        {
            return HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).VirtualPath;
        }

        public virtual bool FileExists(string virtualPath)
        {
            return HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
        }

        public virtual bool TryFileExists(string virtualPath)
        {
            if (IsMalformedVirtualPath(virtualPath))
                return false;

            try
            {
                return FileExists(virtualPath);
            }
            catch (Exception e)
            {
                // _logger.Information("File '{0}' can not be checked for existence. Assuming doesn't exist.".FormatCurrent(virtualPath), e);
                return false;
            }
        }

        public virtual bool DirectoryExists(string virtualPath)
        {
            return HostingEnvironment.VirtualPathProvider.DirectoryExists(virtualPath);
        }

        public virtual void CreateDirectory(string virtualPath)
        {
            Directory.CreateDirectory(MapPath(virtualPath));
        }

        public virtual void DeleteDirectory(string virtualPath)
        {
            Directory.Delete(MapPath(virtualPath));
        }

        public bool IsMalformedVirtualPath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return true;

            if (virtualPath.IndexOf("..", StringComparison.Ordinal) < 0) return false;

            virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
            var rootPrefix = virtualPath.StartsWith("~/") ? "~/" : virtualPath.StartsWith("/") ? "/" : "";

            if (string.IsNullOrEmpty(rootPrefix)) return false;

            var terms = virtualPath.Substring(rootPrefix.Length).Split('/');
            var depth = 0;

            foreach (var term in terms)
            {
                if (term == "..")
                {
                    if (depth == 0)
                    {
                        //_logger.Information("Path '{0}' cannot be made app relative: Too many '..'".FormatCurrent(virtualPath));
                        return true;
                    }
                    depth--;
                }
                else
                {
                    depth++;
                }
            }

            return false;
        }
    }
}