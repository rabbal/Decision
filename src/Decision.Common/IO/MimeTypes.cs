using System;
using System.Collections.Concurrent;
using System.Web;
using Decision.Common.Extensions;
using Microsoft.Win32;

namespace Decision.Common.IO
{
    public static class MimeTypes
    {
        private static readonly ConcurrentDictionary<string, string> _mimeMap =
            new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static string MapNameToMimeType(string fileNameOrExtension)
        {
            return MimeMapping.GetMimeMapping(fileNameOrExtension);
        }

        /// <summary>
        ///     Returns the (dotless) extension for a mime type
        /// </summary>
        /// <param name="mimeType">The mime type</param>
        /// <returns>The corresponding file extension (without dot)</returns>
        public static string MapMimeTypeToExtension(string mimeType)
        {
            if (mimeType.IsEmpty())
                return null;

            return _mimeMap.GetOrAdd(mimeType, k =>
            {
                string result = null;

                try
                {
                    using (var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false))
                    {
                        var value = key?.GetValue("Extension", null);
                        result = value?.ToString().Trim('.');
                    }
                }
                catch (Exception)
                {
                    var parts = mimeType.Split('/');
                    result = parts[parts.Length - 1];
                    switch (result)
                    {
                        case "pjpeg":
                            result = "jpg";
                            break;
                        case "x-png":
                            result = "png";
                            break;
                        case "x-icon":
                            result = "ico";
                            break;
                    }
                }

                return result;
            });
        }
    }
}